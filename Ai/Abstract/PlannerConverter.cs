namespace UniGame.Ecs.Proto.AI.Abstract
{
    using System;
    using Configurations;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public abstract class PlannerConverter : GameObjectConverter,IPlannerConverter, IEntityConverter
    {
        [SerializeField]
        public AiAgentActionId id;
        
        public AiAgentActionId Id => id;

        public void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            if (enabled == false)
                return;
            
            Apply(world, entity);
            OnApply(target,world,entity);
        }
        
        protected virtual void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            
        }

        public virtual void Apply(ProtoWorld world, ProtoEntity entity)
        {
            
        }
    }


    [Serializable]
    public class PlannerConverter<TComponent> : PlannerConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
#if ODIN_INSPECTOR
        [HideLabel]
        [InlineProperty]
#endif
        [SerializeField]
        public TComponent data;
    
        protected sealed override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var component = ref world.GetOrAddComponent<TComponent>(entity);
            data.Apply(ref component);
            OnApplyComponents(target, world, entity);
        }
        
        protected virtual void OnApplyComponents(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
        }

    }
}