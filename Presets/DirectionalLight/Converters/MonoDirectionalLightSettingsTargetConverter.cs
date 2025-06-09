namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Converters
{
    using System;
    using Abstract;
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public class MonoDirectionalLightSettingsTargetConverter : MonoLeoEcsConverter<DirectionalLightSettingsTargetConverter>
    {
        
    }
    
    [Serializable]
    public sealed class DirectionalLightSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        public string id = nameof(DirectionalLightSettingsPresetComponent);
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        public bool showButtons;

#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
#endif 
        public DirectionalLightPresets sourcePreset;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<DirectionalLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);

            idComponent.Value = id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
#endif 
        public void Bake()
        {
            sourcePreset.BakeDirectionalLight();
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
#endif 
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToDirectionalLight();
        }
    }
}