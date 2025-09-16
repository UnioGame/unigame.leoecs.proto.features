﻿namespace UniGame.Ecs.Proto.Effects
{
    using System;
    using System.Collections.Generic;
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Data;
    using Feature;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [Serializable]
    [CreateAssetMenu(menuName = "ECS Proto/Features/Effects/Effects Feature")]
    public sealed class EffectsFeature : BaseLeoEcsFeature
    {
        public int maxEffectsCount = EcsEffectsConfiguration.MAX_EFFECTS_COUNT;
        
#if ODIN_INSPECTOR
        [Header("Effects")]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [InlineEditor]
#endif
        public List<EffectFeature> effectFeatures = new();

        public AddressableValue<EffectsRootConfiguration> effectsRootValue;
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            EcsEffectsConfiguration.MAX_EFFECTS_COUNT = maxEffectsCount;
            
            var world = ecsSystems.GetWorld();
            var worldLifeTime = world.GetWorldLifeTime();
            var configuration = await effectsRootValue.reference
                .LoadAssetInstanceTaskAsync(worldLifeTime,true);
            
            world.SetGlobal(configuration.data);
            
            //create global effect root entity data
            ecsSystems.Add(new CreateEffectGlobalRootsSystem());
            
            ecsSystems.Add(new ValidateEffectsListSystem());
            ecsSystems.Add(new ProcessEffectDurationSystem());
            
            ecsSystems.Add(new ValidateEffectSystem());
            ecsSystems.Add(new CreateEffectSystem());
            
            ecsSystems.DelHere<CreateEffectSelfRequest>();
            
            ecsSystems.Add(new DelayedEffectSystem());
            ecsSystems.Add(new ProcessEffectPeriodicitySystem());

            foreach (var feature in effectFeatures)
            {
                if(feature.isEnabled == false) continue;
                await feature.InitializeAsync(ecsSystems);
            }

            ecsSystems.DelHere<EffectAppliedSelfEvent>();
            ecsSystems.Add(new ProcessAppliedEffectsSystem());

            //====== select effect view parent ==========
            
            //select effect parent by avatar data
            ecsSystems.Add(new SelectAvatarParentTargetsSystem());
            //select effect parent by effect root id
            ecsSystems.Add(new SelectBakedParentByRootIdSystem());
            //select effect from destination entity at runtime if no backed found
            ecsSystems.Add(new SelectChildParentByRootIdSystem());
            //select global effect root entity by effect root id
            ecsSystems.Add(new SelectGlobalParentByRootIdSystem());
            
            //create effect view when effect applied
            ecsSystems.Add(new ShowEffectViewSystem());
            
            ecsSystems.Add(new ProcessEffectViewLifeTimeSystem());
            ecsSystems.Add(new ProcessEffectViewPrepareToDeathSystem());
            ecsSystems.Add(new ProcessEffectViewOwnerSystem());

            ecsSystems.Add(new DestroyEffectViewSystem());
            ecsSystems.Add(new DestroyEffectSystem());
            
            ecsSystems.DelHere<DestroyEffectSelfRequest>();
            ecsSystems.DelHere<RemoveEffectRequest>();
            ecsSystems.DelHere<ApplyEffectSelfRequest>();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void FillEffects()
        {
#if UNITY_EDITOR
            var assets = AssetEditorTools.GetAssets<EffectFeature>();
            foreach (var effect in assets)
            {
                if(effectFeatures.Contains(effect))continue;
                effectFeatures.Add(effect);
            }
            this.MarkDirty();
#endif
        }
    }

    
    public class EcsEffectsConfiguration
    {
        public static int MAX_EFFECTS_COUNT = 200;
    }
}