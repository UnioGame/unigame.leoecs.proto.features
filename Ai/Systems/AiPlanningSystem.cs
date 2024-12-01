namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Service;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AiPlanningSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AiAspect _aiAspect;
        
        private ProtoItExc _filter  = It
            .Chain<AiAgentComponent>()
            .Exc<AiAgentSelfControlComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var agentComponent = ref _aiAspect.AiAgent.Get(entity);
                var plan = agentComponent.PlannerData;
                var actions = agentComponent.PlannedActions;

                //MaxPriorityPlanning(plan, actions);
                PriorityPlanning(plan, actions);
            }
        }

        private void PriorityPlanning(AiPlannerData[] plan,bool[] actions)
        {
            for (var i = 0; i < plan.Length; i++)
            {
                var priority = plan[i].Priority;
                actions[i] = priority>0;
            }
        }
        
        private void MaxPriorityPlanning(AiPlannerData[] plan,bool[] actions)
        {
            var maxPriority = -1f;
            var selectedId = -1;
            for (var i = 0; i < plan.Length; i++)
            {
                var priority = plan[i].Priority;
                if(priority<=maxPriority) continue;

                maxPriority = priority;
                selectedId = i;
            }

            for (var i = 0; i < plan.Length; i++)
                actions[i] = i == selectedId;
        }

    }
}
