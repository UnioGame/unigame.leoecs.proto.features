namespace Game.Ecs.ButtonAction.Components.Events
{
    using System;
    using GameActions.Data;

    /// <summary>
    /// Represents an event associated with a button action.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GameActionEvent
    {
        public int Id;
    }
}