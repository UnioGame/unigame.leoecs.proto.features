namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Converters
{
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public class MonoDirectionalLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [SerializeField]
        public DirectionalLightSettingsSourceConverter directionalLightConverter = new();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            directionalLightConverter.Apply(world, entity);
        }
    }
}