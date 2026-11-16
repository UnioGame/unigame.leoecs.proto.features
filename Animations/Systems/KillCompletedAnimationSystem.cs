namespace UniGame.Ecs.Proto.Animations.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
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
    public sealed class KillCompletedAnimationSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter = It
            .Chain<AnimationCompleteComponent>()
            .Inc<AnimationKillOnComplete>()
            .End();
        private ProtoWorld _world;
        
        private AnimationTimelineAspect _animationAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                if(_animationAspect.KillOnComplete.Has(entity))
                    _world.DelEntity(entity);
            }
        }
    }
}