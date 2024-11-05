namespace Game.Ecs.ButtonAction.Components
{
    using System;

    /// <summary>
    /// Represents a button action component.
    /// </summary>
    /// <typeparam name="TAction">The type of action map.</typeparam>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ButtonActionComponent<TAction>
    {
        public TAction Id;
    }
}