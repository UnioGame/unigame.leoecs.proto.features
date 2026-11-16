namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using System.Collections.Generic;
    using Data;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Список целей движения, которые планировщики набрали на этот кадр. Побеждает цель с наибольшим
    /// приоритетом.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveToGoalComponent : IProtoAutoReset<MoveToGoalComponent>
    {
        public List<MoveToGoalData> Goals;
        
        public void SetHandlers(IProtoPool<MoveToGoalComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref MoveToGoalComponent c)
        {
            c.Goals ??= new List<MoveToGoalData>(8);
            c.Goals.Clear();
        }
    }
}