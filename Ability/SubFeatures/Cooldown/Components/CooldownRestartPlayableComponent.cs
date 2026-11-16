namespace UniGame.Ecs.Proto.Ability.SubFeatures.Cooldown.Components
{
    using System;

    /// <summary>
    /// Шаг способности «запустить кулдаун»: с этого момента пошёл отсчёт.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CooldownRestartPlayableComponent
    {
    }
}