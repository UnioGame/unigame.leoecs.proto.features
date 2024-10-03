namespace UniGame.Ecs.Proto.Ability.SubFeatures.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using FakeTimeline.Components.Requests;
    using Game.Ecs.Core.Components;
    using Game.Ecs.SpineAnimation.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
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
    public class AbilityAnimationSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private AbilityAnimationsAspect _abilityAnimationsAspect;
        private OwnershipAspect _ownershipAspect;
        private SpineAnimationAspect _spineAnimationAspect;

        private ProtoItExc _behaviourFilter = It
            .Chain<AbilityAnimationComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var behaviourEntity in _behaviourFilter)
            {
                ref var requestAnimationComponent = ref _abilityAnimationsAspect.AbilityAnimation.Get(behaviourEntity);
                var playAnimId = requestAnimationComponent.playAnimationId;
                var nextPlayAnimId = requestAnimationComponent.nextPlayAnimationId;
                var timeScale = requestAnimationComponent.timeScale;
                if (!requestAnimationComponent.targetEntity.Unpack(_world, out var unpackedTargetEntity))
                {
                    continue;
                }

                ref var playAnimationSelfRequest = ref _spineAnimationAspect.Play.GetOrAdd(unpackedTargetEntity);
                playAnimationSelfRequest.animationTypeId = playAnimId;
                playAnimationSelfRequest.nextAnimationTypeId = nextPlayAnimId;
                playAnimationSelfRequest.timeScale = timeScale;
            }
        }
    }
}