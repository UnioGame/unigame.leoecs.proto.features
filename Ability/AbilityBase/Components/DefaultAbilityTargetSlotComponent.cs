namespace UniGame.Ecs.Proto.Ability.Components
{
    using System;

    /// <summary>
    /// В каком слоте у владельца лежит основная способность.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DefaultAbilityTargetSlotComponent
    {
        public int Value;
    }
}