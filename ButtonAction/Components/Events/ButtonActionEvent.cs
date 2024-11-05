namespace Game.Ecs.ButtonAction.Components.Events
{
    using System;

    /// <summary>
    /// Represents an event associated with a button action.
    /// </summary>
    /// <typeparam name="TAction">The type of button action.</typeparam>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ButtonActionEvent<TAction>
    {
        public TAction Id;
    }
}