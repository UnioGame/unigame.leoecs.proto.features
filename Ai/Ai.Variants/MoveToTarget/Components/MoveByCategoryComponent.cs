namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;

    /// <summary>
    /// Настройка ИИ «иди к врагам нужной категории»: кого ищем, как далеко видим и насколько ближние цели
    /// важнее дальних.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MoveByCategoryComponent 
        : IApplyableComponent<MoveByCategoryComponent>
    {
        public float MinFilteredTargetPriority;
        
        public MoveFilterData[] FilterData;
        
        /// <summary>
        /// Дистанция при достижении которой приоритет будет расти
        /// Если дистанция больше, то до приоритет == 0
        /// </summary>
        public float EffectiveDistance;

        /// <summary>
        /// Максимальный приоритет который добавляется за близость к цели
        /// </summary>
        public float MaxPriorityByDistance;
                
        public void Apply(ref MoveByCategoryComponent component)
        {
            component.FilterData = FilterData;
            component.EffectiveDistance = EffectiveDistance;
            component.MaxPriorityByDistance = MaxPriorityByDistance;
        }
    }
}