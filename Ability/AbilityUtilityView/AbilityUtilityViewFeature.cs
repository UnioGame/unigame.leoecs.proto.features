namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView
{
    using System;
    using Area.Systems;
    using Cysharp.Threading.Tasks;
    using Highlights.Components;
    using Highlights.Systems;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Ability;
    using Radius.Component;
    using Radius.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Proto Features/Ability/Ability Utility View Feature", 
        fileName = "Ability Utility View Feature")]
    public class AbilityUtilityViewFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();

            GetHighlightSystems(ecsSystems);
            GetRadiusSystems(ecsSystems);
            GetAreaSystems(ecsSystems);
            
            return UniTask.CompletedTask;
        }

        private IProtoSystems GetHighlightSystems(IProtoSystems systems)
        {
            systems.AddSystem(new ProcessInHandAbilityHighlightSystem());
            systems.AddSystem(new ProcessNotInHandAbilityHighlightSystem());
            systems.AddSystem(new ProcessHighlightWhenDeadSystem());
            systems.AddSystem(new ProcessShowHighlightRequestSystem());
            systems.DelHere<ShowHighlightRequest>();
            systems.AddSystem(new ProcessHideHighlightRequestSystem());
            systems.DelHere<HideHighlightRequest>();
            return systems;
        }

        private IProtoSystems GetRadiusSystems(IProtoSystems systems)
        {
            systems.AddSystem(new ProcessInHandAbilityRadiusSystem());
            //systems.AddSystem(new ProcessRadiusAreaAbilitySystem());
            //systems.AddSystem(new ProcessRadiusForTargetAbilitySystem());
            systems.AddSystem(new ProcessNotInHandAbilityRadiusSystem());
            
            systems.AddSystem(new ProcessAggressiveAbilityRadiusSystem());
            systems.AddSystem(new ProcessAggressiveAbilityRadiusWhenDeadSystem());
            systems.AddSystem(new ProcessAbilityRadiusWhenOwnerDeadSystem());
            systems.AddSystem(new ProcessHideRadiusRequestSystem());
            
            systems.DelHere<HideRadiusRequest>();
            
            systems.AddSystem(new ProcessShowRadiusRequestSystem());
            
            systems.DelHere<ShowRadiusRequest>();
            
            return systems;
        }

        private IProtoSystems GetAreaSystems(IProtoSystems systems)
        {
            //systems.AddSystem(new ShowAreaSystem());
            //systems.AddSystem(new UpdateAreaPositionSystem());
            systems.AddSystem(new DestroyAreaByOwnerSystem());
            //systems.AddSystem(new DestroyAreaSystem());

            return systems;
        }
    }
}