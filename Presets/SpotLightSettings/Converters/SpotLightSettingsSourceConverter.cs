namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters
{
    using System;
    using Abstract;
    using Assets;
    using UniGame.Ecs.Proto.Presets.Components;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Ecs.Proto.Presets.Converters;
    using UniGame.LeoEcs.Converter.Runtime;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [Serializable]
    public class SpotLightSettingsSourceConverter : EcsComponentConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif 
        public string targetId = nameof(SpotLightSettingsPresetComponent);
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
        [FoldoutGroup("Spot Light Settings")]
        [HideLabel]
#endif 
        public SpotLightPresets preset = new SpotLightPresets()
        {
            showTargetValue = false
        };

        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<SpotLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }

        private void SearchTarget(bool apply)
        {
            var spotLightsTargets =
                UnityEngine.Object.FindObjectsOfType<MonoSpotLightSettingsTargetConverter>(includeInactive: true);

            var generalLightTargets =
                UnityEngine.Object.FindObjectsOfType<MonoGeneralLightSettingsPresetTargetConverter>(
                    includeInactive: true);

            foreach (var spotLightTarget in spotLightsTargets)
            {
                if (spotLightTarget.converter.id != targetId) continue;

                var sourcePreset = spotLightTarget.converter.sourcePreset;

                preset.SetSourceConverter(apply, sourcePreset);
                break;
            }

            foreach (var spotLightTarget in generalLightTargets)
            {
                if (spotLightTarget.converter.spotLightConverter.id != targetId) continue;

                var sourcePreset = spotLightTarget.converter.spotLightConverter.sourcePreset;

                preset.SetSourceConverter(apply, sourcePreset);
                break;
            }
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void Bake()
        {
            SearchTarget(false);
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void ApplyToTarget()
        {
            SearchTarget(true);
        }
    }
}