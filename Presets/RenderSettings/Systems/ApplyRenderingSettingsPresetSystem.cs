namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using RenderSettings.Aspects;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Apply material preset to target system.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyRenderingSettingsPresetSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;
        private RenderSettingsAspect _renderSettingsAspect;

        private ProtoIt _targetFilter = It
            .Chain<RenderingSettingsPresetComponent>()
            .Inc<PresetTargetComponent>()
            .Inc<PresetApplyingComponent>()
            .Inc<PresetApplyingDataComponent>()
            .Inc<PresetProgressComponent>()
            .End();

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _presetsAspect.PresetApplyingData.Get(targetEntity);
                if(!applyingDataComponent.Source.Unpack(_world,out var sourceEntity))
                    continue;

                ref var targetPresetComponent = ref _renderSettingsAspect.SettingsPreset.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _renderSettingsAspect.SettingsPreset.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _presetsAspect.PresetProgress.GetOrAddComponent(targetEntity);

                var activePreset = targetPresetComponent.Value;
                var sourcePreset = presetComponent.Value;
                
                activePreset.ApplyLerp(activePreset,sourcePreset,progressComponent.Value);
                activePreset.ApplyToRendering();
            }
        }
    }
}