namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Data;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
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
    public sealed class CreateTimeLineAnimationSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        private AnimationToolSystem _animationTool;
        
        private AnimationTimelineAspect _animationAspect;
        private OwnershipAspect _ownershipAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _animationTool = _world.GetGlobal<AnimationToolSystem>();
            
            _filter = _world
                .Filter<CreateAnimationPlayableSelfRequest>()
                .End();
        }
        
        public void Run()
        {
            foreach (var animationEntity in _filter)
            {
                ref var request = ref _animationAspect.CreateSelfAnimation.Get(animationEntity);
                
                ref var durationComponent = ref _animationAspect.Duration.GetOrAddComponent(animationEntity);
                ref var animationComponent = ref _animationAspect.Animation.GetOrAddComponent(animationEntity);
                ref var bindingDataComponent = ref _animationAspect.Binding.GetOrAddComponent(animationEntity);
                ref var targetComponent = ref _animationAspect.Target.GetOrAddComponent(animationEntity);
                ref var activeComponent = ref _animationAspect.Ready.GetOrAddComponent(animationEntity);
                ref var wrapModeComponent = ref _animationAspect.WrapMode.GetOrAddComponent(animationEntity);
                ref var speedComponent = ref _animationAspect.Speed.GetOrAddComponent(animationEntity);
                ref var startTimeComponent = ref _animationAspect.StartTime.GetOrAddComponent(animationEntity);

                speedComponent.Value = request.Speed > 0 ? request.Speed : speedComponent.Value;
                targetComponent.Value = request.Target;
                bindingDataComponent.Value = request.BindingData;
                animationComponent.Value = request.Animation;
                wrapModeComponent.Value = request.WrapMode;
                durationComponent.Value = request.Duration;
                
                request.Owner.AddChild(animationEntity, _world);
                
                var playable = animationComponent.Value;

                durationComponent.Value = durationComponent.Value <= 0
                    ? playable == null ? 0f : (float)playable.duration
                    : durationComponent.Value;
            }
        }
    }
}