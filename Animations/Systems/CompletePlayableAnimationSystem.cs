namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;


#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CompletePlayableAnimationSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        
        private AnimationTimelineAspect _animationAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<StopAnimationSelfRequest>()
                .Inc<AnimationTargetComponent>()
                .End();
        } 
        
        public void Run()
        {
            foreach (var animationEntity in _filter)
            {
                _animationAspect.Playing.TryRemove(animationEntity);
                _animationAspect.Complete.GetOrAddComponent(animationEntity);
            }
        }
    }
}