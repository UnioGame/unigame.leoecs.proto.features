namespace UniGame.Ecs.Proto.Presets.Converters
{
    using UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters;
    using UniGame.Ecs.Proto.Presets.FogShaderSettings.Converters;

    using UnityEngine;
    using System;
    using Abstract;
    using DirectionalLight.Converters;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public class MonoGeneralLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        public GeneralLightSettingsPresetSourceConverter generalLightConverter = new();
        
        public sealed override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            generalLightConverter.Apply(world, entity);
        }
    }


    [Serializable]
    public sealed class GeneralLightSettingsPresetSourceConverter : EcsComponentConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Rendering")]
        [OnValueChanged(nameof(AutoUpdate))]
#endif 
        public RenderingSettingsSourceConverter renderingConverter = 
            new RenderingSettingsSourceConverter() { showButtons = false };

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Fog Shader")]
        [OnValueChanged(nameof(AutoUpdate))]
#endif 
        public FogShaderSettingsSourceConverter fogShaderConverter = 
            new FogShaderSettingsSourceConverter() { showButtons = false };
        
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Spot Light")]
        [OnValueChanged(nameof(AutoUpdate))]
#endif 
        public SpotLightSettingsSourceConverter spotLightConverter = 
            new SpotLightSettingsSourceConverter() { showButtons = false };
        
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Directional Light")]
        [OnValueChanged(nameof(AutoUpdate))]
#endif 
        public DirectionalLightSettingsSourceConverter directionalLightConverter = 
            new DirectionalLightSettingsSourceConverter() { showButtons = false };


        public override void Apply(ProtoWorld world, ProtoEntity entity)
        {
            if(renderingConverter.isEnabled)
                renderingConverter.Apply(world, world.NewEntity());
            if(fogShaderConverter.isEnabled)
                fogShaderConverter.Apply(world, world.NewEntity());
            if(spotLightConverter.isEnabled)
                spotLightConverter.Apply(world, world.NewEntity()); 
            if(directionalLightConverter.isEnabled)
                directionalLightConverter.Apply(world, world.NewEntity());
        }

        private void AutoUpdate()
        {
            fogShaderConverter.ApplyToTarget();
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
#endif 

        public void Bake()
        {
            fogShaderConverter.Bake();
            renderingConverter.Bake();
            spotLightConverter.Bake();
            directionalLightConverter.Bake();
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
#endif 
        public void ApplyToTarget()
        {
            fogShaderConverter.ApplyToTarget();
            renderingConverter.ApplyToTarget();
            spotLightConverter.ApplyToTarget();
            directionalLightConverter.ApplyToTarget();
        }
    }
}