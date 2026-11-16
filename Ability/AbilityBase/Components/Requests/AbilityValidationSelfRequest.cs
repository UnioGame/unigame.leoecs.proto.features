namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;

    /// <summary>
    /// Компонент с запросом на применение умения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityValidationSelfRequest
    {
    }
}