namespace UniGame.Ecs.Proto.Presets.DirectionalLight.Converters
{
    using Assets;
    using System;
    using Abstract;
    using UniGame.Ecs.Proto.Presets.Components;
    using UniGame.Ecs.Proto.Presets.Converters;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class DirectionalLightSettingsSourceConverter : EcsComponentConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif
        public string targetId = nameof(DirectionalLightSettingsPresetComponent);
        
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
        [FoldoutGroup("Directional Light Settings")]
        [HideLabel]
#endif
        public DirectionalLightPresets preset = new DirectionalLightPresets()
        {
            showTargetValue = false
        };

        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<DirectionalLightSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }

        private void SearchTarget(bool apply)
        {
            var directionalLightsTargets =
                UnityEngine.Object.FindObjectsOfType<MonoDirectionalLightSettingsTargetConverter>(includeInactive: true);

            var generalLightTargets =
                UnityEngine.Object.FindObjectsOfType<MonoGeneralLightSettingsPresetTargetConverter>(
                    includeInactive: true);

            foreach (var directionalLightTarget in directionalLightsTargets)
            {
                if (directionalLightTarget.converter.id != targetId) continue;

                var sourcePreset = directionalLightTarget.converter.sourcePreset;

                preset.SetSourceConverter(apply, sourcePreset);
                break;
            }

            foreach (var directionalLightTarget in generalLightTargets)
            {
                if (directionalLightTarget.converter.directionalLightConverter.id != targetId) continue;

                var sourcePreset = directionalLightTarget.converter.directionalLightConverter.sourcePreset;

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