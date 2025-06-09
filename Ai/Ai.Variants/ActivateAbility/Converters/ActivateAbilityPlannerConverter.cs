namespace UniGame.Ecs.Proto.GameAi.ActivateAbility.Converters
{
    using System;
    using System.Collections.Generic;
    using AI.Abstract;
    using Game.Code.Ai.ActivateAbility;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [Serializable]
    public class ActivateAbilityPlannerConverter : 
        PlannerConverter<ActivateAbilityPlannerComponent>, 
        ILeoEcsGizmosDrawer
    {
#if ODIN_INSPECTOR
        [InlineProperty]
#endif 
        [SerializeReference] 
        private List<IAbilityByConverter> _converters = new List<IAbilityByConverter>();

        protected override void OnApplyComponents(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            //world.AddComponentToEntity<MoveToGoalComponent>(entity);

            foreach (var converter in _converters)
                converter.Apply(world, entity);
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in _converters)
            {
                if (converter is not ILeoEcsGizmosDrawer drawer)
                    continue;
                drawer.DrawGizmos(target);
            }
        }
    }
}