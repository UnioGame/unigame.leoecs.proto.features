namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using Game.Modules.leoecs.proto.features.Ability.AbilityBase.Components;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;


    /// <summary>
    /// Компонент состояния оценки умения.
    /// </summary>
    public struct AbilityEvaluationComponent : IProtoAutoReset<AbilityEvaluationComponent>
    {
        public float EvaluateTime;
        
        public void SetHandlers(IProtoPool<AbilityEvaluationComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref AbilityEvaluationComponent c)
        {
            c.EvaluateTime = 0.0f;
        }
    }
}