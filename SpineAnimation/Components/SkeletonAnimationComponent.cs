namespace Game.Ecs.SpineAnimation.Components
{
    using System;
    using Spine.Unity;

    /// <summary>
    /// Component that holds a reference to a SkeletonAnimation.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SkeletonAnimationComponent
    {
        public SkeletonAnimation SkeletonAnimation;
    }
}