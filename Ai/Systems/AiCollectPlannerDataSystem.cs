namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine.Pool;

    /// <summary>
    /// collect ai agents info
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AiCollectPlannerDataSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AiAspect _aiAspect;
        
        private ProtoItExc _filter  = It
            .Chain<AiAgentComponent>()
            .Exc<AiAgentSelfControlComponent>()
            .End();
        
        public void Run()
        {
            foreach (var agentEntity in _filter)
            {
                ref var agentComponent = ref _aiAspect.AiAgent.Get(agentEntity);
                ref var dataComponent = ref _aiAspect.AiAgentPlanning.Add(agentEntity);
                
                var aiPlan = dataComponent.AiPlan;
                dataComponent.AiPlan = aiPlan ?? ListPool<AiAgentPlanningData>.Get();
                aiPlan = dataComponent.AiPlan;
                
                var agentPlan = agentComponent.PlannerData;

                for (var id = 0; id < agentPlan.Length; id++)
                {
                    var data = agentPlan[id];

                    var actionPriority = data.Priority;
                    if(actionPriority < 0) continue;

                    var planItem = new AiAgentPlanningData
                    {
                        Priority = data.Priority,
                        ActionId = id,
                    };
                    
                    aiPlan.Add(planItem);
                }
            }
        }

    }
}
