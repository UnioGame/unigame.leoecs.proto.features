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
    public struct SequenceDataComponent : IEcsAutoReset<SequenceDataComponent>
    {
        public ProtoPackedEntity Target;
        public SequenceActionData[] Actions;
        public SequenceActionResult[] Results;
        public int Length;
        public int ActiveAction;
        
        public void AutoReset(ref SequenceDataComponent c)
        {
            c.Results?.Despawn();
            c.Length = 0;
            c.ActiveAction = 0;
        }
    }
}