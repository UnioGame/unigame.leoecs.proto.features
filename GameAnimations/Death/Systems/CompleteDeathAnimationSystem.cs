namespace UniGame.Ecs.Proto.Gameplay.Death.Systems
{
    using System;
    using Animations.Aspects;
    using Aspects;
    using Core.Death.Components;
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
    public sealed class CompleteDeathAnimationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DeathAspect _deathAspect;
        private AnimationTimelineAspect _animationAspect;
        
        private ProtoIt _filter = It
            .Chain<DeadAnimationEvaluateComponent>()
            .Inc<DeathAnimationComponent>()
            .Inc<DeathCompletedComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var playableDirector = ref _deathAspect.Director.Get(entity);

                if (_deathAspect.Champion.Has(entity)) continue;
                
                playableDirector.Value.Stop();
                playableDirector.Value.playableAsset = null;
                    
                _deathAspect.Evaluate.Del(entity);
                _deathAspect.AwaitDeath.TryRemove(entity);
            }
        }
    }
}