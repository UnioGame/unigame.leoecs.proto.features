namespace Game.Modules.SequenceActions.Components
{
    using System;
    using Abstract;
    using Data;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto.QoL;
    using UniGame.Runtime.ObjectPool.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SequenceComponent
    {
        public ProtoPackedEntity Target;
        public ISequenceAction Action;
    }
}