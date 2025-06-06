namespace UniGame.Ecs.Proto.GameAi.ActivateAbility.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using TargetSelection;

    /// <summary>
    /// data for ability range action
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityRangeActionDataComponent : IProtoAutoReset<AbilityRangeActionDataComponent>
    {
        public ProtoPackedEntity[] Values;
        public int Count;
        
        public void SetHandlers(IProtoPool<AbilityRangeActionDataComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AbilityRangeActionDataComponent c)
        {
            c.Values ??= new ProtoPackedEntity[TargetSelectionData.MaxTargets];
            c.Count = 0;
        }
    }
}