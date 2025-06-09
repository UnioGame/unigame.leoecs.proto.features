namespace UniGame.Ecs.Proto.Presets.Light.Converters
{
    using System;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoLightPresetSourceConverter : MonoLeoEcsConverter<LightPresetSourceConverter>
    {
    }
    
    [Serializable]
    public sealed class LightPresetSourceConverter : LeoEcsConverter
    {
        public string targetId = nameof(LightPresetComponent);
        
        public bool useLight;
        
#if ODIN_INSPECTOR
        [HideIf(nameof(useLight))]
        [FoldoutGroup("Light Preset")]
        [InlineProperty]
        [HideLabel]
#endif 

        public LightPreset lightPreset;

#if ODIN_INSPECTOR
        [ShowIf(nameof(useLight))]
#endif 
        public Light light;
        
        public float duration;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<LightPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            var lightTargetPreset = useLight && light != null ? light.CreateLightPreset() : lightPreset;

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = lightTargetPreset;
            durationComponent.Value = duration;
        }
    }
}