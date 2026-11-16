namespace UniGame.Ecs.Proto.Ability.Components
{
    using System;

    /// <summary>
    /// Уровень способности.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityLevelComponent
    {
        public int Level;
    }
}