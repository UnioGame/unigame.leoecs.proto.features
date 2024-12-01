namespace UniGame.Ecs.Proto.Gameplay.Death.Systems
{
    using System;
    using Animations.Aspects;
    using Aspects;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Core.Death.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class EvaluateDeathAnimationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DeathAspect _deathAspect;
        private AnimationTimelineAspect _animationAspect;
        
        private ProtoItExc _filter = It
            .Chain<DeadAnimationEvaluateComponent>()
            .Inc<DeathAnimationComponent>()
            .Inc<PlayableDirectorComponent>()
            .Exc<DeathCompletedComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var evaluate = ref _deathAspect.Evaluate.Get(entity);

                if (!evaluate.Value.Unpack(_world, out var animationEntity))
                {
                    _deathAspect.Completed.Add(entity);
                    continue;
                }

                if (!_animationAspect.Complete.Has(animationEntity)) continue;
                
                _deathAspect.Completed.Add(entity);
            }
        }
    }
}