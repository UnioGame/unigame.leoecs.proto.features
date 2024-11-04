namespace Game.Ecs.SpineAnimation.Components.Requests
{
    using System;
    using Data.AnimationType;
    using Leopotam.EcsLite;

    /// <summary>
    /// Request to play a spine animation on the entity itself.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PlaySpineAnimationSelfRequest : IEcsAutoReset<PlaySpineAnimationSelfRequest>
    {
        public AnimationTypeId AnimationTypeId;
        public AnimationTypeId NextAnimationTypeId;
        public float TimeScale;
        
        public void AutoReset(ref PlaySpineAnimationSelfRequest c)
        {
            c.NextAnimationTypeId = AnimationTypeId.Idle;
            c.TimeScale = 1f;
        }
    }
}