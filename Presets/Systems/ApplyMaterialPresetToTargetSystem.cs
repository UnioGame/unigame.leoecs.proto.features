namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public class ApplyMaterialPresetToTargetSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;

        private ProtoIt _targetFilter = It
            .Chain<MaterialPresetComponent>()
            .Inc<PresetTargetComponent>()
            .Inc<PresetApplyingComponent>()
            .Inc<PresetProgressComponent>()
            .Inc<PresetApplyingDataComponent>()
            .End();

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var dataComponent = ref _presetsAspect.PresetApplyingData.Get(targetEntity);
                if(!dataComponent.Source.Unpack(_world,out var sourceEntity))
                    continue;

                ref var targetMaterialData = ref _presetsAspect.MaterialPreset.Get(targetEntity);
                ref var sourceMaterialData = ref _presetsAspect.MaterialPreset.Get(sourceEntity);

                ref var progressComponent = ref _presetsAspect.PresetProgress.Get(targetEntity);

                var targetMaterial = targetMaterialData.Value;
                var sourceMaterial = sourceMaterialData.Value;
                
                targetMaterialData.Value.Lerp(targetMaterial,sourceMaterial,progressComponent.Value);
            }
        }
    }
}