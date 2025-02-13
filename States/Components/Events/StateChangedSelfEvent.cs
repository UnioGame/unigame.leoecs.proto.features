namespace Game.Ecs.State.Components.Events
{
    using System;

    /// <summary>
    /// Event indicating a state change.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StateChangedSelfEvent
    {
        public int FromStateId;
        public int NewId;
    }
}