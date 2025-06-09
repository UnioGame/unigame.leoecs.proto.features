namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Components
{
    using System;
    using AI.Service;

    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [Serializable]
    public struct MoveToTargetPlannerComponent : IApplyableComponent<MoveToTargetPlannerComponent>
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [SerializeField]
        public AiPlannerData PlannerData;

        public void Apply(ref MoveToTargetPlannerComponent component)
        {
            component.PlannerData = PlannerData;
        }
    }
}
