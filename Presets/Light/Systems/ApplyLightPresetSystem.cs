namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Light.Aspects;
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
    public class ApplyLightPresetSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;
        private UnityAspect _unityAspect;
        private LightAspect _lightAspect;


        private ProtoIt _targetFilter = It
            .Chain<LightPresetComponent>()
            .Inc<PresetTargetComponent>()
            .Inc<LightComponent>()
            .Inc<PresetApplyingComponent>()
            .Inc<PresetApplyingDataComponent>()
            .Inc<PresetProgressComponent>()
            .End();

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var applyingDataComponent = ref _presetsAspect.PresetApplyingData.Get(targetEntity);
                if (!applyingDataComponent.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var targetPresetComponent = ref _lightAspect.LightPreset.GetOrAddComponent(targetEntity);
                ref var lightComponent = ref _lightAspect.Light.GetOrAddComponent(targetEntity);
                ref var presetComponent = ref _lightAspect.LightPreset.GetOrAddComponent(sourceEntity);
                ref var progressComponent = ref _presetsAspect.PresetProgress.GetOrAddComponent(targetEntity);

                ref var activePreset = ref targetPresetComponent.Value;
                ref var sourcePreset = ref presetComponent.Value;

                var light = lightComponent.Value;

                LightPresetTools.Lerp(light, ref activePreset, ref sourcePreset, progressComponent.Value);
            }
        }
    }
}