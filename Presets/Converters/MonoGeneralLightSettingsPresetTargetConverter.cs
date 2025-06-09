namespace UniGame.Ecs.Proto.Presets.Converters
{
    using UniGame.Ecs.Proto.Presets.DirectionalLight.Converters;
    using UniGame.Ecs.Proto.Presets.SpotLightSettings.Converters;
    using Abstract;
    using System;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Presets.FogShaderSettings.Converters;

    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    public class MonoGeneralLightSettingsPresetTargetConverter :
        MonoLeoEcsConverter<GeneralLightSettingsPresetTargetConverter>
    {
    }

    [Serializable]
    public sealed class GeneralLightSettingsPresetTargetConverter : LeoEcsConverter, IPresetAction
    {
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Rendering")]
#endif 
        public RenderingSettingsTargetConverter renderingConverter =
            new RenderingSettingsTargetConverter() { showButtons = false };

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Fog Shader")]
#endif 
        public FogShaderSettingsTargetConverter fogShaderConverter =
            new FogShaderSettingsTargetConverter() { showButtons = false };

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Spot Light")]
#endif 
        public SpotLightSettingsTargetConverter spotLightConverter =
            new SpotLightSettingsTargetConverter() { showButtons = false };

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 10)]
        [InlineProperty]
        [HideLabel]
        [Title("Directional Light")]
#endif 
        public DirectionalLightSettingsTargetConverter directionalLightConverter =
            new DirectionalLightSettingsTargetConverter() { showButtons = false };

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            if (renderingConverter.IsEnabled)
                renderingConverter.Apply(target, world, world.NewEntity());
            if (fogShaderConverter.IsEnabled)
                fogShaderConverter.Apply(target, world, world.NewEntity());
            if (spotLightConverter.IsEnabled)
                spotLightConverter.Apply(target, world, world.NewEntity());
            if (directionalLightConverter.IsEnabled)
                directionalLightConverter.Apply(target, world, world.NewEntity());
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
#endif 
        public void Bake()
        {
            renderingConverter.Bake();
            fogShaderConverter.Bake();
            spotLightConverter.Bake();
            directionalLightConverter.Bake();
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
#endif 
        public void ApplyToTarget()
        {
            renderingConverter.ApplyToTarget();
            fogShaderConverter.ApplyToTarget();
            spotLightConverter.ApplyToTarget();
            directionalLightConverter.ApplyToTarget();
        }
    }
}