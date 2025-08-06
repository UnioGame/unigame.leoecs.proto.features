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
    public class MarkOwnerCharacteristicChangedSystem<TCharacteristic> : IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private OwnershipAspect _ownershipAspect;
        private GameCharacteristicAspect<TCharacteristic> _characteristicAspect;

        private ProtoIt _filter = It
            .Chain<CharacteristicValueChangedEvent<TCharacteristic>>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var changedEvent = ref _characteristicAspect.OnValueChanged.Get(entity);
                if(!_world.Unpack(changedEvent.Owner,out var ownerEntity)) continue;

                ref var ownerCharacteristic = ref _characteristicAspect
                    .OnOwnerCharacteristicChanged
                    .GetOrAddComponent(ownerEntity);
                
                ownerCharacteristic.Characteristic = changedEvent.Characteristic;
                ownerCharacteristic.Value = changedEvent.Value;
                ownerCharacteristic.PreviousValue = changedEvent.PreviousValue;
            }
        }
    }
}