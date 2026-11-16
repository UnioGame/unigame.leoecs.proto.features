namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;

    /// <summary>
    /// Способность сейчас используется: её таймлайн ещё играет. Пока метка висит, применить её снова
    /// нельзя.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityUsingComponent
    {
    }
}