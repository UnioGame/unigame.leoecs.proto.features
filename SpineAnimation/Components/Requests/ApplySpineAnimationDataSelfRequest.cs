namespace Game.Ecs.SpineAnimation.Components.Requests
{
    using System;
    using Spine;

    /// <summary>
    /// Request to apply Spine animation data to an entity itself.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ApplySpineAnimationDataSelfRequest
    {
        public Event Event;
    }
}