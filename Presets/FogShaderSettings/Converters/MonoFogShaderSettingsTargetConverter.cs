namespace UniGame.Ecs.Proto.Presets.FogShaderSettings.Converters
{
    using Abstract;
    using System;
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoFogShaderSettingsTargetConverter : MonoLeoEcsConverter<FogShaderSettingsTargetConverter>
    {
    }

    [Serializable]
    public sealed class FogShaderSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        private string _id = nameof(FogShaderSettingsPresetComponent);
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        public bool showButtons;

#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
#endif 
        public FogShaderPresets sourcePreset;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<FogShaderSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);

            idComponent.Value = _id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void Bake()
        {
            sourcePreset.BakeActiveFogShaderSettings();
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToShader();
        }
    }
}