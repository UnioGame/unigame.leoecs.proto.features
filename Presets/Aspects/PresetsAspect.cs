namespace UniGame.Ecs.Proto.Presets.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class PresetsAspect : EcsAspect
    {
        public ProtoPool<MaterialPresetComponent> MaterialPreset;
        public ProtoPool<MaterialPresetSourceComponent> MaterialPresetSource;

        public ProtoPool<ActivePresetSourceComponent> ActivePresetSource;
        public ProtoPool<PresetActivatedComponent> PresetActivated;
        public ProtoPool<PresetApplyingComponent> PresetApplying;
        public ProtoPool<PresetApplyingDataComponent> PresetApplyingData;
        public ProtoPool<PresetComponent> Preset;
        public ProtoPool<PresetDurationComponent> PresetDuration;
        public ProtoPool<PresetIdComponent> PresetIdC;
        public ProtoPool<PresetProgressComponent> PresetProgress;
        public ProtoPool<PresetSourceComponent> PresetSource;
        public ProtoPool<PresetTargetComponent> PresetTarget;
    }
}