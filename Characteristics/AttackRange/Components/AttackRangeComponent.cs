namespace UniGame.Ecs.Proto.Characteristics.CriticalChance.Components
{
    using System;

    /// <summary>
    /// Дальность атаки.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackRangeComponent
    {
        public float Value;
    }
}