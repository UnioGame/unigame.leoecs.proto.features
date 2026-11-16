namespace UniGame.Ecs.Proto.AI.Abstract
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Service;

    /// <summary>
    /// Планировщик ИИ: оценивает, насколько агенту сейчас нужно своё действие, и записывает приоритет в
    /// план агента.
    /// </summary>
    public interface IAiPlannerSystem : IAiPlannerSwitched
    {
        public UniTask Initialize(int id,IProtoSystems ecsSystems);
       
        void ApplyPlanningResult(IProtoSystems systems, ProtoEntity entity, AiPlannerData data);
    }

    public interface IAiPlannerSwitched
    {
        void RemoveComponent(IProtoSystems systems, ProtoEntity entity);
    }
    
}