namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Converters
{
    using System;
    using Abstract;
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    public class MonoDirectionalLightSettingsTargetConverter : MonoLeoEcsConverter<DirectionalLightSettingsTargetConverter>
    {
        
    }
    
    [Serializable]
    public sealed class DirectionalLightSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
        [ShowIf(nameof(IsEnabled))]
        public string id = nameof(DirectionalLightSettingsPresetComponent);
        [ShowIf(nameof(IsEnabled))]
        public bool showButtons;

        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
        public DirectionalLightPresets sourcePreset;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<DirectionalLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);

            idComponent.Value = id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }

        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
        public void Bake()
        {
            sourcePreset.BakeDirectionalLight();
        }

        [Button]
        [ShowIf("@this.showButtons && this.IsEnabled")]
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToDirectionalLight();
        }
    }
}