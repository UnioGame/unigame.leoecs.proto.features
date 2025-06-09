namespace UniGame.Ecs.Proto.AI.Abstract
{
    using System;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public abstract class ComponentPlannerConverter : EcsComponentConverter, IEntityConverter
    {
        public void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            Apply(world, entity);
            OnApply(target, world, entity);
        }

        protected virtual void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {

        }
    }
    
    [Serializable]
    public class ComponentPlannerConverter<TComponent> : ComponentPlannerConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
#if ODIN_INSPECTOR
        [FoldoutGroup(nameof(data))]
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public TComponent data;
    
        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var component = ref world.GetOrAddComponent<TComponent>(entity);
            data.Apply(ref component);
        }

        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            
        }
    }
}