namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters
{
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    public sealed class MonoSpotLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public SpotLightSettingsSourceConverter spotLightConverter = new SpotLightSettingsSourceConverter();

        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            spotLightConverter.Apply(world, entity);
        }
    }
}