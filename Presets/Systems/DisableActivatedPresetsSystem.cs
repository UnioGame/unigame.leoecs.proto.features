namespace UniGame.Ecs.Proto.Presets.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DisableActivatedPresetsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private PresetsAspect _presetsAspect;

        private ProtoIt _sourceFilter = It
            .Chain<PresetComponent>()
            .Inc<ActivePresetSourceComponent>()
            .Inc<PresetActivatedComponent>()
            .End();

        public void Run()
        {
            foreach (var sourceEntity in _sourceFilter)
            {
                _presetsAspect.ActivePresetSource.Del(sourceEntity);
                _presetsAspect.PresetActivated.Del(sourceEntity);

                break;
            }
        }
    }
}