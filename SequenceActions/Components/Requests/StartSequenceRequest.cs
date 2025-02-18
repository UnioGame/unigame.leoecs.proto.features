namespace Game.Modules.SequenceActions.Components.Requests
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct StartSequenceRequest
    {
        public ProtoPackedEntity Target;
        public bool AutoDestroy;
        public ISequenceAction Action;
    }
}