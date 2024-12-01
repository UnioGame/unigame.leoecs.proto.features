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
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine.Playables;

    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessDeathAnimationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AnimationTimelineAspect _animationAspect;
        private DeathAspect _deathAspect;

        private ProtoItExc _filter = It
            .Chain<PrepareToDeathComponent>()
            .Inc<DeathAnimationComponent>()
            .Inc<PlayableDirectorComponent>()
            .Exc<DeadAnimationEvaluateComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                var ownerEntity = entity.PackEntity(_world);
                ref var deadAnimation = ref _deathAspect.Animation.Get(entity);
                ref var evaluate = ref _deathAspect.Evaluate.Add(entity);

                var animationEntity = _world.NewEntity();
                var packedAnimationEntity = animationEntity.PackEntity(_world);
                ref var createAnimation = ref _animationAspect.CreateSelfAnimation.Add(animationEntity);
                ref var playAnimation = ref _animationAspect.PlaySelf.Add(animationEntity);

                createAnimation.Animation = deadAnimation.Animation;
                createAnimation.Duration = (float)deadAnimation.Animation.duration;
                createAnimation.WrapMode = DirectorWrapMode.Hold;
                createAnimation.Owner = ownerEntity;
                createAnimation.Target = ownerEntity;
                createAnimation.Speed = 1f;

                playAnimation.Duration = createAnimation.Duration;
                playAnimation.Speed = createAnimation.Speed;

                evaluate.Value = packedAnimationEntity;

                _deathAspect.Disabled.GetOrAddComponent(entity);
                _deathAspect.AwaitDeath.Add(entity);
            }
        }
    }
}