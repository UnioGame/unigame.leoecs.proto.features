namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Configurations;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Service;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AiCleanUpPlanningDataSystem : IProtoRunSystem, IProtoInitSystem
    {
        private IReadOnlyList<AiActionData> _actionData;
        private ProtoWorld _world;
        private IProtoSystems _systems;
        private AiAspect _aiAspect;

        private ProtoIt _filter = It
            .Chain<AiAgentComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _systems = systems;
            _world = systems.GetWorld();
        }

        public AiCleanUpPlanningDataSystem(IReadOnlyList<AiActionData> actionData)
        {
            _actionData = actionData;
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var agentComponent = ref _aiAspect.AiAgent.Get(entity);
                var actionsMap = agentComponent.PlannedActions;

                for (var i = 0; i < actionsMap.Length; i++)
                {
                    //reset priority
                    ref var data = ref agentComponent.PlannerData[i];
                    data.Priority = AiConstants.PriorityNever;

                    //update action status
                    var actions = agentComponent.PlannedActions;
                    var actionStatus = actions[i];
                    var selfController = _aiAspect.AiAgentSelfControl.Has(entity);
                    agentComponent.PlannedActions[i] = selfController && actionStatus;

                    //remove ai system components
                    var planner = _actionData[i].planner;
                    planner.RemoveComponent(_systems, entity);
                }
            }
        }
    }
}