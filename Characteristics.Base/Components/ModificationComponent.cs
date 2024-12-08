namespace UniGame.Ecs.Proto.Characteristics.Base.Components
{
    using System;

    /// <summary>
    /// modification component value
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ModificationComponent
    {
        public float BaseValue;
        public bool IsPercent;
        public bool AllowedSummation;
        public int Counter;
    }
}