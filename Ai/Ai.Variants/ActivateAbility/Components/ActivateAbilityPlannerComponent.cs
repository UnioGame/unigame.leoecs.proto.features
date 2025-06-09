namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using GameLayers.Category;

    using UniGame.Ecs.Proto.AI.Service;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct ActivateAbilityPlannerComponent : IApplyableComponent<ActivateAbilityPlannerComponent>
    {
        /// <summary>
        /// Action Planner Data
        /// </summary>
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [SerializeField]
        public AiPlannerData PlannerData;

        public void Apply(ref ActivateAbilityPlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }

    [Serializable]
#if ODIN_INSPECTOR
    [HorizontalGroup(nameof(CategoryPriority),LabelWidth = 40)]
#endif 
    public struct CategoryPriority
    {
        public CategoryId Category;
        public float Value;
    }
}