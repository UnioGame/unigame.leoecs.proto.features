namespace UniGame.Ecs.Proto.Animations.Aspects
{
    using System;
    using Characteristics.Duration.Components;
    using Components;
    using Components.Requests;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    [Serializable]
    public class PlayableAnimatorAspect : EcsAspect
    {
        public ProtoPool<PlayableDirectorComponent> Director;
        public ProtoPool<AnimatorPlayingComponent> PlayingTarget;
        public ProtoPool<DurationComponent> Duration;
        
        //optional / statuses
        public ProtoPool<AnimationPlayingComponent> Playing;


        public ProtoPool<PlayOnDirectorSelfRequest> Play;
        public ProtoPool<StopAnimationSelfRequest> StopSelf;
    }
}