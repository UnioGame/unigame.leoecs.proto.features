namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

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
    public class CompletePresetProgressSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;

        private ProtoIt _targetFilter = It
            .Chain<PresetApplyingComponent>()
            .Inc<PresetProgressComponent>()
            .Inc<PresetApplyingDataComponent>()
            .End();

        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var progressComponent = ref _presetsAspect.PresetProgress.Get(targetEntity);
                var progress = progressComponent.Value;

                if (progress < 1f) continue;
                
                _presetsAspect.PresetApplying.Del(targetEntity);
                _presetsAspect.PresetApplyingData.Del(targetEntity);
            }
        }
    }
}