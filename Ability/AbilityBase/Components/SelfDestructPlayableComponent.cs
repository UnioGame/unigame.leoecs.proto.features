namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;

    /// <summary>
    /// Шаг способности «самоуничтожение»: когда приходит его время, владелец способности умирает.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SelfDestructPlayableComponent
    {
    }
}
