namespace UniGame.Ecs.Proto.Presets.FogShaderSettings.Converters
{
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;

    using UniGame.LeoEcs.Shared.Extensions;
    using System;
    using Abstract;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    /// <summary>
    /// Fog shader convertor
    /// </summary>
    [Serializable]
    public class FogShaderSettingsSourceConverter : EcsComponentConverter,IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif
        public string targetId = nameof(FogShaderSettingsPresetComponent);
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif
        public float duration;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif
        public bool showButtons;

#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
        [FoldoutGroup("Fog Shader Settings")]
        [HideLabel]
#endif
        public FogShaderPresets preset = new FogShaderPresets();

        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<FogShaderSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }

#if ODIN_INSPECTOR
        [Button] 
        [ShowIf(nameof(showButtons))]
#endif
        public void Bake()
        {
            preset.BakeActiveFogShaderSettings();
        }

#if ODIN_INSPECTOR
        [Button] 
        [ShowIf(nameof(showButtons))]
#endif
        public void ApplyToTarget()
        {
            preset.ApplyToShader();
        }
    }
}