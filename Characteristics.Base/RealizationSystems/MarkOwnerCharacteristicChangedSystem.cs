namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Aspects;
    using Components;
    using Components.Events;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// update characteristic owner value by changed status
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MarkOwnerCharacteristicChangedSystem<TCharacteristic> : IProtoRunSystem, IProtoInitSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoPool<CharacteristicValueChangedEvent<TCharacteristic>> _eventPool;
        private ProtoPool<OwnerCharacteristicChangedSelfEvent<TCharacteristic>> _ownerEventPool;
        
        private ProtoIt _filter = It
            .Chain<CharacteristicValueChangedEvent<TCharacteristic>>()
            .End();

        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _eventPool = _world.GetPool<CharacteristicValueChangedEvent<TCharacteristic>>();
            _ownerEventPool = _world.GetPool<OwnerCharacteristicChangedSelfEvent<TCharacteristic>>();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var changedEvent = ref _eventPool.Get(entity);
                if(!_world.Unpack(changedEvent.Owner,out var ownerEntity)) continue;

                ref var ownerCharacteristic = ref _ownerEventPool
                    .GetOrAddComponent(ownerEntity);
                
                ownerCharacteristic.Characteristic = changedEvent.Characteristic;
                ownerCharacteristic.Value = changedEvent.Value;
                ownerCharacteristic.PreviousValue = changedEvent.PreviousValue;
            }
        }

    }
}