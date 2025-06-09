namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Converters
{
    using System;
    using System.Collections.Generic;
    using AI.Abstract;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [Serializable]
    public class MoveToTargetPlannerConverter : PlannerConverter<MoveToTargetPlannerComponent>,ILeoEcsGizmosDrawer
    {
#if ODIN_INSPECTOR
        [InlineProperty]
#endif 
        [SerializeReference]
        public List<IMoveByConverter> converters = new List<IMoveByConverter>();

        protected override void OnApplyComponents(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            world.AddComponent<MoveToGoalComponent>(entity);
            world.AddComponent<MoveToPoiGoalsComponent>(entity);

            foreach (var converter in converters)
                converter.Apply(world,entity);
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in converters)
            {
                if(converter is not ILeoEcsGizmosDrawer drawer)
                    continue;
                drawer.DrawGizmos(target);
            }
        }
    }
}
