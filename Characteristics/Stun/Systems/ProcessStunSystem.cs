namespace UniGame.Ecs.Proto.Characteristics.Stun.Systems
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Characteristics.Base.Components;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessStunSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private StunAspect _stunAspect;
        
        private ProtoIt _characteristicFilter = It
            .Chain<CharacteristicChangedComponent<StunComponent>>()
            .End();

        public void Run()
        {
            foreach (var characteristicEntity in _characteristicFilter)
            {
                ref var characteristicChangedComponent = ref _stunAspect.Changed.Get(characteristicEntity);
                ref var sourceCounterComponent = ref _stunAspect.SourceCounter.GetOrAddComponent(characteristicEntity);
                if (characteristicChangedComponent.Value == 0f)
                {
                    if (--sourceCounterComponent.value < 1 && _stunAspect.Stun.Has(characteristicEntity))
                    {
                        _stunAspect.Stun.Del(characteristicEntity);
                    }
                }
                else
                {
                    sourceCounterComponent.value++;
                    _stunAspect.Stun.GetOrAddComponent(characteristicEntity);
                }
            }
        }
    }
}