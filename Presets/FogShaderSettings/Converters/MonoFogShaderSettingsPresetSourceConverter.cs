namespace UniGame.Ecs.Proto.Presets.FogShaderSettings.Converters
{
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoFogShaderSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [SerializeField]
        public FogShaderSettingsSourceConverter fogShaderConverter = new FogShaderSettingsSourceConverter();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            fogShaderConverter.Apply(world, entity);
        }
    }
}