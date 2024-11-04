namespace Game.Ecs.SpineAnimation.Components
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Data.AnimationType;

    /// <summary>
    /// Component that holds a dictionary of Spine animations.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SpineAnimationsComponent
    {
        public Dictionary<AnimationTypeId, SpineAnimationSettings> Animations;
    }
}