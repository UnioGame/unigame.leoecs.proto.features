namespace UniGame.Ecs.Proto.Presets.Light.Aspects
{
    using System;
    using Components;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class LightAspect : EcsAspect
    {
        public ProtoPool<LightPresetComponent> LightPreset;
        public ProtoPool<LightComponent> Light;
    }
}