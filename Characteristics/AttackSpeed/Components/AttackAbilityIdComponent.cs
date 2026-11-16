namespace UniGame.Ecs.Proto.Characteristics.AttackSpeed.Components
{
    using System;

    /// <summary>
    /// Id способности, которую считать атакой: на неё действует скорость атаки.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackAbilityIdComponent
    {
        public int Value;
    }
}