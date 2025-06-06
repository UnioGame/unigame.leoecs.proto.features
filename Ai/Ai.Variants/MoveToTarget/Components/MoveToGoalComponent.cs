namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System.Collections.Generic;
    using Data;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


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