namespace UniGame.Ecs.Proto.Presets.Converters
{
    using Abstract;

    using System;
    using Assets;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoRenderingSettingsTargetConverter : MonoLeoEcsConverter<RenderingSettingsTargetConverter>
    {
    }
    
    [Serializable]
    public sealed class RenderingSettingsTargetConverter : LeoEcsConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        public string id = nameof(RenderingSettingsPresetComponent);
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
#endif 
        public bool showButtons;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(IsEnabled))]
        [HideLabel]
#endif 
        public RenderingSettingsPreset sourcePreset;

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<RenderingSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var presetTargetComponent = ref world.GetOrAddComponent<PresetTargetComponent>(entity);
            
            idComponent.Value = id.GetHashCode();
            sourceComponent.Value = sourcePreset;
        }
        
#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void Bake()
        {
            sourcePreset.BakeActiveRenderingSettings();
        }

#if ODIN_INSPECTOR
        [Button]
        [ShowIf(nameof(showButtons))]
#endif 
        public void ApplyToTarget()
        {
            sourcePreset.ApplyToRendering();
        }
    }
}