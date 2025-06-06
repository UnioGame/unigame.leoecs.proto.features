namespace UniGame.Ecs.Proto.TargetSelection.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;


    /// <summary>
    /// cache for range selection
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SqrRangeTargetsResultComponent : IProtoAutoReset<SqrRangeTargetsResultComponent>
    {
        public ProtoPackedEntity[] Values;
        public int Count;
        
        public void SetHandlers(IProtoPool<SqrRangeTargetsResultComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref SqrRangeTargetsResultComponent c)
        {
            c.Values ??= new ProtoPackedEntity[TargetSelectionData.MaxTargets];
            c.Count = 0;
        }
    }
}