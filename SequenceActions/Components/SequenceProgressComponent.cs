namespace Game.Modules.SequenceActions.Components
{
    using System;
    using UnityEngine.Serialization;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SequenceProgressComponent
    {
        public bool IsComplete;
        public bool IsError;
        public float MaxProgress;
        public float CompleteProgress;
        public float ProgressWeight;
        public float Progress;
    }
}