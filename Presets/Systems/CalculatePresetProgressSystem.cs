namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Time.Service;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// update preset progression
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CalculatePresetProgressSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;
        
        private ProtoIt _targetFilter = It
            .Chain<PresetApplyingComponent>()
            .Inc<PresetApplyingDataComponent>()
            .Inc<PresetProgressComponent>()
            .End();
        
        public void Run()
        {
            foreach (var targetEntity in _targetFilter)
            {
                ref var dataComponent = ref _presetsAspect.PresetApplyingData.Get(targetEntity);

                var timePassed = GameTime.Time - dataComponent.StartTime;
                var duration = dataComponent.Duration;
                var progress = duration <= 0 ? 1f : timePassed / duration;
                
                ref var progressComponent = ref _presetsAspect.PresetProgress.Get(targetEntity);
                progressComponent.Value = progress;
            }
        }
    }
}