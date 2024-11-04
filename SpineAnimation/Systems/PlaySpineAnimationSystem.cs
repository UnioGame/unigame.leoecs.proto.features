namespace Game.Ecs.SpineAnimation.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Components.Requests;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Represents a system for playing Spine animations.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class PlaySpineAnimationSystem : IEcsRunSystem
    {
        private ProtoWorld _world;
        private SpineAnimationAspect _spineAnimationAspect;

        private ProtoIt _filter = It
            .Chain<SkeletonAnimationComponent>()
            .Inc<SpineAnimationsComponent>()
            .Inc<PlaySpineAnimationSelfRequest>()
            .Inc<SkeletonAnimationInitializedComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var skeletonAnimationComponent = ref _spineAnimationAspect.SkeletonAnimation.Get(entity);
                ref var spineAnimationsComponent = ref _spineAnimationAspect.SpineAnimations.Get(entity);
                ref var playSpineAnimationSelfRequest = ref _spineAnimationAspect.Play.Get(entity);

                var targetAnimation = playSpineAnimationSelfRequest.AnimationTypeId;
                var timeScale = playSpineAnimationSelfRequest.TimeScale;
                var skeletonAnimation = skeletonAnimationComponent.SkeletonAnimation;

                var animations = spineAnimationsComponent.Animations;
                var animationSettings = animations.GetValueOrDefault(targetAnimation);

                var trackIndex = animationSettings.trackIndex;
                var animation = animationSettings.animation;
                var loop = animationSettings.loop;

                var animationTrack = skeletonAnimation.AnimationState.SetAnimation(trackIndex, animation, loop);
                animationTrack.TimeScale = timeScale;

                if (loop) continue;
                
                var nextAnimationTypeId = playSpineAnimationSelfRequest.NextAnimationTypeId;

                var idleAnimation = animations.GetValueOrDefault(nextAnimationTypeId);
                skeletonAnimation.AnimationState.AddAnimation(trackIndex, idleAnimation.animation,
                    idleAnimation.loop, 0);
            }
        }
    }
}