namespace Game.Ecs.ButtonAction.Components.Requests
{
    using System;
    using GameActions.Data;

    /// <summary>
    /// request to activate game action.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GameActionRequest
    {
        public int Id;
    }
}