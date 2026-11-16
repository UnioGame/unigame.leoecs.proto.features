namespace UniGame.Ecs.Proto.AI.Components
{
    using System;
    using Configurations;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Service;

    /// <summary>
    /// Мозг агента: конфиг ИИ, какие действия ему доступны, какие выбраны на этот кадр и с каким
    /// приоритетом.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AiAgentComponent : IProtoAutoReset<AiAgentComponent>
    {
        public AiAgentConfiguration Configuration;
        
        // <summary>
        /// доступные экшены агенту по конфигу
        /// </summary>
        public bool[] AvailableActions;
        
        /// <summary>
        /// состояние экшенов на агенте
        /// </summary>
        public bool[] PlannedActions;

        public AiPlannerData[] PlannerData;
        
        public void SetHandlers(IProtoPool<AiAgentComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AiAgentComponent c)
        {
            c.Configuration = null;
            if (c.PlannedActions != null)
            {
                var statues = c.PlannedActions;
                for (var i = 0; i < statues.Length; i++)
                    statues[i] = false;
            }

            if (c.PlannerData != null)
            {
                for (var i = 0; i < c.PlannerData.Length; i++)
                {
                    ref var data = ref c.PlannerData[i];
                    data.Priority = -1;
                }
            }
        }
    }
}
