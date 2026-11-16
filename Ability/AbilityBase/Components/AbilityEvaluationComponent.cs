namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using UniGame.Proto.Ability;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Компонент состояния оценки умения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
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