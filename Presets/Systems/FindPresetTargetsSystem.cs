namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Time.Service;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
    public class FindPresetTargetsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;

        private ProtoItExc _targetFilter = It
            .Chain<PresetIdComponent>()
            .Inc<PresetTargetComponent>()
            .Exc<PresetApplyingComponent>()
            .Exc<PresetApplyingDataComponent>()
            .End();

        private ProtoIt _sourceFilter = It
            .Chain<PresetIdComponent>()
            .Inc<PresetSourceComponent>()
            .Inc<ActivePresetSourceComponent>()
            .End();

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var targetIdComponent = ref _presetsAspect.PresetIdC.Get(targetEntity);

                foreach (var sourceEntity in _sourceFilter)
                {
                    ref var sourceIdComponent = ref _presetsAspect.PresetIdC.Get(sourceEntity);
                    if (sourceIdComponent.Value != targetIdComponent.Value) continue;

                    ref var progressComponent = ref _presetsAspect.PresetProgress.GetOrAddComponent(targetEntity);
                    ref var applyingDataComponent = ref _presetsAspect.PresetApplyingData.Add(targetEntity);
                    ref var applyingComponent = ref _presetsAspect.PresetApplying.Add(targetEntity);

                    progressComponent.Value = 0f;
                    applyingDataComponent.Duration = 0;
                    applyingDataComponent.StartTime = GameTime.Time;

                    if (_presetsAspect.PresetDuration.Has(sourceEntity))
                    {
                        ref var durationComponent = ref _presetsAspect.PresetDuration.Get(sourceEntity);
                        applyingDataComponent.Duration = durationComponent.Value;
                    }

                    applyingDataComponent.Source = sourceEntity.PackEntity(_world);

                    _presetsAspect.PresetActivated.GetOrAddComponent(sourceEntity);

                    break;
                }
            }
        }
    }
}