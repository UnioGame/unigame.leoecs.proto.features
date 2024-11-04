namespace Game.Ecs.SpineAnimation.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// Aspect for Spine animation system.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class SpineAnimationAspect : EcsAspect
    {
        // Component that holds a reference to a SkeletonAnimation.
        public ProtoPool<SkeletonAnimationComponent> SkeletonAnimation;
        // Component indicating that a skeleton animation has been initialized.
        public ProtoPool<SkeletonAnimationInitializedComponent> Initialized;
        // Component that holds a dictionary of Spine events.
        public ProtoPool<SpineAnimationEventsComponent> AnimationEvents;
        // Component that holds a dictionary of Spine animations.
        public ProtoPool<SpineAnimationsComponent> SpineAnimations;
        
        // Represents an event associated with a Spine animation.
        public ProtoPool<SpineAnimationEventSelf> Event;

        // Request to apply Spine animation data to an entity itself.
        public ProtoPool<ApplySpineAnimationDataSelfRequest> Apply;
        // Request to play a spine animation on the entity itself.
        public ProtoPool<PlaySpineAnimationSelfRequest> Play;
    }
}