namespace UniGame.Ecs.Proto.Presets.SpotLightSettings.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class SpotLightSettingsAspect : EcsAspect
    {
        public ProtoPool<SpotLightSettingsPresetComponent> SettingsPreset;
    }
}