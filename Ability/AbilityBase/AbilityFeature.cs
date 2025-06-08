namespace UniGame.Ecs.Proto.Ability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Components;
    using Common.Systems;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Sirenix.OdinInspector;
    using SubFeatures;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using Runtime.Utils;
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif
    
    [Serializable]
    public sealed class AbilityFeature : EcsFeature
    {
        [SerializeReference]
        [ListDrawerSettings(ListElementLabelName = "@FeatureName")]
        public List<AbilitySubFeature> abilityFeatures;
        
        [SerializeReference]
        [ListDrawerSettings(ListElementLabelName = "@FeatureName")]
        public List<AbilityPluginFeature> abilityPlugins;

        protected override async UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            var subFeatures = abilityFeatures
                .Where(x => x.isActive)
                .ToList();
            
            ecsSystems.DelHere<AbilityVelocityEvent>();

            foreach (var feature in subFeatures)
            {
                await feature.OnInitializeSystems(ecsSystems);
            }
            
            //setup ability in hand by slot
            ecsSystems.Add(new ProcessSetInHandAbilityBySlotRequestSystem());
            ecsSystems.DelHere<SetInHandAbilityBySlotSelfRequest>();
            
            //handle ApplyAbilityRequest and apply ability by slot
            ecsSystems.Add(new ProcessApplyAbilityBySlotRequestSystem());
            ecsSystems.DelHere<ApplyAbilityBySlotSelfRequest>();

            //activate ability by id with request ActivateAbilityByIdRequest, take it in hand and use
            ecsSystems.Add(new ActivateAbilityByIdSystem());
            ecsSystems.Add(new ActivateAbilitySystem());

            foreach (var feature in subFeatures)
            {
                await feature.OnStartSystems(ecsSystems);
            }

            ecsSystems.Add(new SetAbilityNotActiveWhenDeadSystem());
            //if non default slot ability in use, discard set in hand request
            ecsSystems.Add(new DiscardSetInHandWhileExecutingAbilitySystem());
            ecsSystems.Add(new DiscardAbilityEffectMilestonesSystem());

            foreach (var feature in subFeatures)
            {
                await feature.OnCompleteAbilitySystems(ecsSystems);
            }

            //remove event after whole loop
            ecsSystems.DelHere<AbilityCompleteSelfEvent>();
            //mark ability as completed and fire AbilityCompleteSelfEvent
            ecsSystems.Add(new CompleteAbilitySystem());
            ecsSystems.DelHere<CompleteAbilitySelfRequest>();

            //set ability in hand by ability entity
            ecsSystems.Add(new SetInHandAbilityRequestSystem());
            ecsSystems.DelHere<SetInHandAbilitySelfRequest>();
                
            //add ability system to update in hand ability state
            foreach (var feature in subFeatures)
            {
                await feature.OnAfterInHandSystems(ecsSystems);
            }
            
            //additional actions before apply ability
            foreach (var feature in subFeatures)
            {
                await feature.OnBeforeApplyAbility(ecsSystems);
            }
            
            //apply ability by request, ability must be in hand and owned by entity
            ecsSystems.Add(new ApplyAbilityRequestSystem());
            ecsSystems.DelHere<ApplyAbilitySelfRequest>();

            foreach (var feature in subFeatures)
            {
                await feature.OnRevokeSystems(ecsSystems);
            }

            foreach (var feature in subFeatures)
            {
                await feature.OnUtilitySystems(ecsSystems);
            }
            
            //activate ability execution
            ecsSystems.DelHere<AbilityStartUsingSelfEvent>();
            ecsSystems.Add(new ApplyAbilitySystem());
            ecsSystems.DelHere<AbilityValidationSelfRequest>();

            //check is any ability activated and mark it with AbilityInProcessingComponent
            ecsSystems.Add(new UpdateAbilityProcessingStatusSystem());
            
            //include on activate systems
            foreach (var feature in subFeatures)
            {
                await feature.OnActivateSystems(ecsSystems);
            }
            
            ecsSystems.Add(new EvaluateAbilitySystem());

            foreach (var feature in subFeatures)
            {
                await feature.OnEvaluateAbilitySystem(ecsSystems);
            }

            ecsSystems.DelHere<ApplyAbilityEffectsSelfRequest>();
            ecsSystems.Add(new CreateApplyAbilityEffectsRequestSystem());
            ecsSystems.Add(new AbilityUnlockSystem());
            
            ecsSystems.Add(new ApplyPauseAbilityRequestSystem());
            ecsSystems.DelHere<PauseAbilityRequest>();
            ecsSystems.Add(new RemovePauseAbilityRequestSystem());
            
            ecsSystems.DelHere<RemovePauseAbilityRequest>();
            ecsSystems.DelHere<AbilityUnlockEvent>();

            foreach (var feature in subFeatures)
            {
                await feature.OnPreparationApplyEffectsSystems(ecsSystems);
            }

            foreach (var feature in subFeatures)
            {
                await feature.OnApplyEffectsSystems(ecsSystems);
            }

            //remove ability activation request
            ecsSystems.DelHere<ActivateAbilityByIdRequest>();
            ecsSystems.DelHere<ActivateAbilityRequest>();

            foreach (var feature in subFeatures)
            {
                await feature.OnLastAbilitySystems(ecsSystems);
            }

            foreach (var abilityPlugin in abilityPlugins)
            {
                await abilityPlugin.InitializeAsync(ecsSystems);
            }
        }

        [Button(DirtyOnClick = true)]
        private void Fill()
        {
#if UNITY_EDITOR
            var features = TypeCache.GetTypesDerivedFrom<AbilitySubFeature>();
            abilityFeatures.RemoveAll(x => x == null);
            var typeSet = abilityFeatures.ToDictionary(x => x.GetType());

            foreach (var featureType in features)
            {
                if(featureType.IsInterface || featureType.IsAbstract) continue;
                if(featureType.HasDefaultConstructor() == false) continue;
                if(typeSet.ContainsKey(featureType)) continue;
                
                var feature = featureType.CreateWithDefaultConstructor() as AbilitySubFeature;
                abilityFeatures.Add(feature);
            }
            
            var plugins = TypeCache.GetTypesDerivedFrom<AbilityPluginFeature>();
            abilityPlugins.RemoveAll(x => x == null);
            var pluginsMap = abilityPlugins.ToDictionary(x => x.GetType());

            foreach (var featureType in plugins)
            {
                if(featureType.IsInterface || featureType.IsAbstract) continue;
                if(featureType.HasDefaultConstructor() == false) continue;
                if(pluginsMap.ContainsKey(featureType)) continue;
                
                var feature = featureType.CreateWithDefaultConstructor() as AbilityPluginFeature;
                abilityPlugins.Add(feature);
            }
#endif
        }
    }
}