namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Events;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsLite;
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
    public class DetectCharacteristicChangedSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private EcsFilter _filter;

        private OwnershipAspect _ownershipAspect;
        
        private ProtoPool<CharacteristicValueChangedEvent<TCharacteristic>> _resultEventPool;
        private ProtoPool<CharacteristicComponent<TCharacteristic>> _characteristicValuePool;
        
        private ProtoPool<MinValueComponent> _minPool;
        private ProtoPool<CharacteristicBaseValueComponent> _baseValuePool;
        private ProtoPool<CharacteristicValueComponent> _valuePool;
        private ProtoPool<MaxValueComponent> _maxPool;
        private ProtoPool<CharacteristicChangedComponent<TCharacteristic>> _changedPool;
        private ProtoPool<CharacteristicChangedComponent> _changedCharacteristicPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<CharacteristicChangedComponent>()
                .Inc<CharacteristicOwnerComponent<TCharacteristic>>()
                .Inc<CharacteristicValueComponent>()
                .Inc<OwnerLinkComponent>()
                .End();
            
            _resultEventPool = _world.GetPool<CharacteristicValueChangedEvent<TCharacteristic>>();
            _changedPool = _world.GetPool<CharacteristicChangedComponent<TCharacteristic>>();
            _characteristicValuePool = _world.GetPool<CharacteristicComponent<TCharacteristic>>();

            _minPool = _world.GetPool<MinValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _changedCharacteristicPool = _world.GetPool<CharacteristicChangedComponent>();
            _valuePool = _world.GetPool<CharacteristicValueComponent>();
        }

        public void Run()
        {
            foreach (var characteristicEntity in _filter)
            {
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(characteristicEntity);
                if(!ownerLinkComponent.Value.Unpack(_world,out var ownerEntity))
                    continue;
                
                ref var characteristicChangedComponent = ref _changedPool.GetOrAddComponent(ownerEntity);
                ref var changedComponent = ref _changedCharacteristicPool.Get(characteristicEntity);

                characteristicChangedComponent.Value = changedComponent.Value;
                characteristicChangedComponent.PreviousValue = changedComponent.PreviousValue;
                
                ref var characteristicValueComponent = ref _valuePool.Get(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Get(characteristicEntity);
                ref var maxValueComponent = ref _maxPool.Get(characteristicEntity);
                ref var minValueComponent = ref _minPool.Get(characteristicEntity);
                
                ref var characteristicComponent = ref _characteristicValuePool.Get(ownerEntity);
                characteristicComponent.Value = characteristicValueComponent.Value;
                characteristicComponent.MinValue = minValueComponent.Value;
                characteristicComponent.MaxValue = maxValueComponent.Value;
                characteristicComponent.BaseValue = baseValueComponent.Value;
                    
                var newEventEntity = _world.NewEntity();
                ref var newEventComponent = ref _resultEventPool.Add(newEventEntity);
                
                newEventComponent.Owner = ownerLinkComponent.Value;
                newEventComponent.Characteristic = _world.PackEntity(characteristicEntity);
                newEventComponent.Value = characteristicValueComponent.Value;
                newEventComponent.PreviousValue = changedComponent.PreviousValue;
            }
        }
    }
}