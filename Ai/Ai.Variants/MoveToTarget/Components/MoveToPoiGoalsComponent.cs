namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
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
        public Dictionary<ProtoEntity, MoveToGoalData> GoalsLinks;

        public void SetHandlers(IProtoPool<MoveToPoiGoalsComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref MoveToPoiGoalsComponent c)
        {
            c.GoalsLinks ??= new Dictionary<ProtoEntity, MoveToGoalData>(8);
            c.GoalsLinks.Clear();
        }
    }
}