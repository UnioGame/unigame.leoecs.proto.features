namespace UniGame.Ecs.Proto.TargetSelection.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Unity.Mathematics;

    /// <summary>
    /// points cound for KD Tree
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct KDTreeDataComponent : IProtoAutoReset<KDTreeDataComponent>
    {
        public ProtoPackedEntity[] PackedEntities;
        public float3[] Values;
        public int Count;
        
        public void SetHandlers(IProtoPool<KDTreeDataComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref KDTreeDataComponent c)
        {
            c.Values ??= new float3[TargetSelectionData.MaxAgents];
            c.PackedEntities ??= new ProtoPackedEntity[TargetSelectionData.MaxAgents];
            c.Count = 0;
        }
    }
}