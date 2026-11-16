namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using Abstract;
    using Aspects;
    using Cysharp.Threading.Tasks;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Service;
     
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Основа для планировщиков ИИ: знает свой id в плане, проверяет, доступен ли планировщик агенту, и
    /// записывает результат планирования.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public abstract class BasePlannerSystem<TComponent>: 
        IAiPlannerSystem,IProtoRunSystem
        where TComponent : struct
    {
        protected int _id;
        protected AiAspect _aiAspect;

        public int Id => _id;
        
        public async UniTask Initialize(int id,IProtoSystems ecsSystems)
        {
            _id = id;
            await OnInitialize(id, ecsSystems);
            ecsSystems.Add(this);
        }

        public abstract void Run();

        public bool IsPlannerEnabledForEntity(ProtoEntity entity)
        {
            ref var aiAgentComponent = ref _aiAspect.AiAgent.Get(entity);
            var availableActions = aiAgentComponent.AvailableActions;
            return availableActions.Length > _id && availableActions[_id];
        }
        
        public void ApplyPlanningResult(IProtoSystems systems, ProtoEntity entity, AiPlannerData data)
        {
            ref var aiAgentComponent = ref _aiAspect.AiAgent.Get(entity);
            
            var resultData = aiAgentComponent.PlannerData;
            resultData[_id] = data;
        }
        
        public void RemoveComponent(IProtoSystems systems, ProtoEntity entity)
        {
            systems.TryRemoveComponent<TComponent>(entity);
        }

        protected virtual UniTask OnInitialize(int id, IProtoSystems systems) => UniTask.CompletedTask;

    }
}
