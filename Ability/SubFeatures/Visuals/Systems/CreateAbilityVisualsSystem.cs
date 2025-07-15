﻿namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Systems
{
    using System;
    using Ability.Aspects;
    using Aspects;
    using Components;
    using Effects;
    using FakeTimeline.Aspects;
    using FakeTimeline.Components.Requests;
    using UniGame.Proto.Ownership;
    using GameResources.Aspects;
    using GameResources.Systems;
    using LeoEcs.Bootstrap;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateAbilityVisualsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private GameResourceAspect _gameResourceAspect;
        private AbilityVisualsAspects _abilityVisualsAspects;
        private OwnershipAspect _ownershipAspect;
        private AbilityAspect _abilityAspect;
        private UnityAspect _unityAspect;
        private TimelineAspect _timelineAspect;
        
        private ProtoIt _playableFilter = It
            .Chain<AbilityVisualsPlayableComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .End();

        public void Run()
        {
            foreach (var playableEntity in _playableFilter)
            {
                ref var executeComponent = ref _timelineAspect.TimelineExecute.Get(playableEntity);
                if (!executeComponent.TimelineContextEntity.Unpack(_world, out var contextEntity))
                {
                    continue;
                }

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Get(contextEntity);
                
                ref var visualPlayableComponent = ref _abilityVisualsAspects.AbilityVisuals.Get(playableEntity);
                var resourceId = visualPlayableComponent.assetIdentification;
                var spawnPositionType = visualPlayableComponent.spawnPosition;
                var isBoneBound = visualPlayableComponent.boneBound;
                var packedTargetEntity = abilityContextComponent.FirstTarget();

                if (!packedTargetEntity.Unpack(_world, out var targetEntity))
                {
                    continue;
                }
                
                var spawnPositionTransform = targetEntity.GetViewInstance(_world, spawnPositionType);
                var targetTransform = isBoneBound ? spawnPositionTransform : null;
                _gameResourceAspect.Spawn(default, resourceId, spawnPositionTransform.position);
            }
        }
    }
}