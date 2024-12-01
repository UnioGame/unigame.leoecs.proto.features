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
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Система удаляет для система экшенов их компоненты с данными,
    /// тем самым включая/выключая логику для сущности агента.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AiUpdatePlanningActionsStatusSystem : IProtoRunSystem, IProtoInitSystem
    {
        private IReadOnlyList<AiActionData> _actionData;
        private ProtoWorld _world;
        private IProtoSystems _systems;
        private AiAspect _aiAspect;

        private ProtoIt _filter = It
            .Chain<AiAgentComponent>()
            .End();

        public AiUpdatePlanningActionsStatusSystem(IReadOnlyList<AiActionData> actionData)
        {
            _actionData = actionData;
        }

        public void Init(IProtoSystems systems)
        {
            _systems = systems;
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var agentComponent = ref _aiAspect.AiAgent.Get(entity);
                var actionsActions = agentComponent.PlannedActions;
                var availableActions = agentComponent.PlannedActions;

                for (var i = 0; i < actionsActions.Length; i++)
                {
                    var actionEnabled = actionsActions[i] && availableActions[i];
                    var dataItem = _actionData[i];
                    var planner = dataItem.planner;

                    if (actionEnabled)
                        continue;

                    planner.RemoveComponent(_systems, entity);
                }
            }
        }
    }
}