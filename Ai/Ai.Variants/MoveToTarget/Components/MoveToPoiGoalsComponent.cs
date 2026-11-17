namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using Leopotam.EcsProto.QoL;
    using System.Collections.Generic;
    using Data;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Какие точки интереса агент уже посетил, а какие ещё нет.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveToPoiGoalsComponent : IProtoAutoReset<MoveToPoiGoalsComponent>
    {
        // key is the packed POI entity: a plain ProtoEntity id can be reused by a new entity after deletion
        public Dictionary<ProtoPackedEntity, MoveToGoalData> GoalsLinks;

        public void SetHandlers(IProtoPool<MoveToPoiGoalsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref MoveToPoiGoalsComponent c)
        {
            c.GoalsLinks ??= new Dictionary<ProtoPackedEntity, MoveToGoalData>(8);
            c.GoalsLinks.Clear();
        }
    }
}