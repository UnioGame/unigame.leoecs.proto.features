namespace UniGame.Ecs.Proto.Presets.Converters
{
    using System;
    using Abstract;
    using Assets;
    using Components;
    using Leopotam.EcsProto;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public sealed class MonoRenderingSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [SerializeField]
        public RenderingSettingsSourceConverter renderingConverter = new RenderingSettingsSourceConverter();
        
        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            renderingConverter.Apply(world, entity);
        }
    }

    [Serializable]
    public sealed class RenderingSettingsSourceConverter : EcsComponentConverter,IPresetAction
    {
#if ODIN_INSPECTOR
        [ShowIf(nameof(isEnabled))]
#endif 
        public string targetId = nameof(RenderingSettingsPresetComponent);
        
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
        [FoldoutGroup("Rendering Settings")]
        [HideLabel]
#endif 
        public RenderingSettingsPreset preset;

#if ODIN_INSPECTOR
        [Button] 
        [ShowIf(nameof(showButtons))]
#endif 
        public void Bake()
        {
            preset.BakeActiveRenderingSettings();
        }    
        
#if ODIN_INSPECTOR
        [Button] 
        [ShowIf(nameof(showButtons))]
#endif 
        public void ApplyToTarget()
        {
            preset.ApplyToRendering();
        }
        
        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<RenderingSettingsPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            idComponent.Value = targetId.GetHashCode();
            dataComponent.Value = preset;
            durationComponent.Value = duration;
        }
    }
}