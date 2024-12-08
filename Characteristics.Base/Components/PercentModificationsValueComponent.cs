namespace UniGame.Ecs.Proto.Characteristics.Base.Components
{
    using System;

    /// <summary>
    /// Percent Value Component
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct PercentModificationsValueComponent
    {
        public float Value;
    }
}