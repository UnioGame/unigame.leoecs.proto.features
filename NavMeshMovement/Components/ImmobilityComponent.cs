namespace UniGame.Ecs.Proto.Movement.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Сущность обездвижена (оглушение, заморозка и т. п.): системы движения её пропускают. Счётчик
    /// считает, сколько источников сейчас держат её на месте.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ImmobilityComponent : IProtoAutoReset<ImmobilityComponent>
    {
        public int BlockSourceCounter;
        
        public void SetHandlers(IProtoPool<ImmobilityComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref ImmobilityComponent c)
        {
            c.BlockSourceCounter = 0;
        }
    }
}