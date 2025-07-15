﻿namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Systems
{
    using System;
    using Aspects;
    using Components;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessHighlightWhenDeadSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        
        private ProtoIt _filter = It
            .Chain<OwnerDestroyedEvent>()
            .Inc<HighlightStateComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var highlightedState = ref _abilityUtilityViewAspect.HighlightState.Get(entity);
                foreach (var packedEntity in highlightedState.Highlights)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _abilityUtilityViewAspect.HideHighlight.Add(hideRequestEntity);
                    
                    hideRequest.Source = entity.PackEntity(_world);
                    hideRequest.Destination = packedEntity.Key;
                }
            }
        }
    }
}