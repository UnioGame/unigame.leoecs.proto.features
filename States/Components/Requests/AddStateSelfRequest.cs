namespace Game.Ecs.State.Components.Requests
{
    using System;

    /// <summary>
    /// Struct representing a request to add a state to the current entity.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AddStateSelfRequest
    {
        public int State;
    }
}