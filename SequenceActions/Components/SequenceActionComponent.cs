namespace Game.Modules.SequenceActions.Components
{
    using System;
    using System.Threading;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Data;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SequenceActionComponent
    {
        public ISequenceAction Action;
        public UniTask Task;
        public CancellationToken Token;
    }
}