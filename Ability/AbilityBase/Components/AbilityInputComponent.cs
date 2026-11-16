namespace UniGame.Ecs.Proto.Ability.Components
{
    using System;

    /// <summary>
    /// Способность управляется вводом игрока, а не ИИ.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityInputComponent
    {
        public bool IsUserInput;
    }
}