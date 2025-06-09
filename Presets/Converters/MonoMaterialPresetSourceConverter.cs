namespace UniGame.Ecs.Proto.Presets.Converters
{
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoMaterialPresetSourceConverter : MonoLeoEcsConverter
    {
#if ODIN_INSPECTOR
        [HideLabel]
        [InlineProperty]
#endif 
        public MaterialPresetSourceConverter converter = new MaterialPresetSourceConverter();
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            converter.Apply(world,entity);
        }
    }
}