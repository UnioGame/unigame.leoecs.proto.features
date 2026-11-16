namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;

    /// <summary>
    /// Скорости бега для анимации: по ним анимация движения понимает, насколько быстро бежит персонаж.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementAnimationInfoComponent
    {
        public float RunSpeed;
        public float MaxRunSpeed;
    }
}