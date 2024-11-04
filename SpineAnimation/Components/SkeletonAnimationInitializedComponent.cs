namespace Game.Ecs.SpineAnimation.Components
{
    using System;

    /// <summary>
    /// Component indicating that a skeleton animation has been initialized.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SkeletonAnimationInitializedComponent
    {
        
    }
}