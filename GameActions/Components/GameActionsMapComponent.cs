namespace Game.Modules.leoecs.proto.features.GameActions.Components
{
    using System;
    using Ecs.GameActions.Data;
    using UnityEngine.Serialization;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct GameActionsMapComponent
    {
        public int[] Actions;
        public string[] Names;
        public bool[] Status;
        public int Length;
    }
}