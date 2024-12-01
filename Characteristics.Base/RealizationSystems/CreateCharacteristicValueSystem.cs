namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create new characteristic value entity for target
    /// by request CreateCharacteristicRequest<TCharacteristic>
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateCharacteristicValueSystem<TCharacteristic> 
        : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;

        private OwnershipAspect _ownershipAspect;
        
        private ProtoPool<CreateCharacteristicRequest<TCharacteristic>> _requestPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _characteristicLinkPool;
        private ProtoPool<CharacteristicValueComponent> _characteristicPool;
        private ProtoPool<MinValueComponent> _minValuePool;
        private ProtoPool<MaxValueComponent> _maxValuePool;
        private ProtoPool<CharacteristicBaseValueComponent> _baseValuePool;
        private ProtoPool<CharacteristicOwnerComponent<TCharacteristic>> _characteristicsOwnerPool;
        private ProtoPool<CharacteristicComponent<TCharacteristic>> _characteristicValuePool;
        private ProtoPool<CharacteristicDefaultValueComponent> _defaultValuePool;
        private ProtoPool<CharacteristicPreviousValueComponent> _previousValuePool;
        private ProtoPool<CharacteristicChangedComponent> _changedPool;
        private ProtoPool<PercentModificationsValueComponent> _modificationPercentPool;
        private ProtoPool<MaxLimitModificationsValueComponent> _maxLimitModificationsPool;
        private ProtoPool<BaseModificationsValueComponent> _valueModificationsPool;
        private ProtoPool<CharacteristicChangedComponent<TCharacteristic>> _ownerChangedPool;

        private ProtoIt _requestFilter = It
            .Chain<CreateCharacteristicRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestPool = _world.GetPool<CreateCharacteristicRequest<TCharacteristic>>();
            
            _characteristicLinkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _ownerChangedPool = _world.GetPool<CharacteristicChangedComponent<TCharacteristic>>();
            _characteristicsOwnerPool = _world.GetPool<CharacteristicOwnerComponent<TCharacteristic>>();
            
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _minValuePool = _world.GetPool<MinValueComponent>();
            _maxValuePool = _world.GetPool<MaxValueComponent>();
            _baseValuePool = _world.GetPool<CharacteristicBaseValueComponent>();
            _characteristicValuePool = _world.GetPool<CharacteristicComponent<TCharacteristic>>();
            _changedPool = _world.GetPool<CharacteristicChangedComponent>();
            _defaultValuePool = _world.GetPool<CharacteristicDefaultValueComponent>();
            _previousValuePool = _world.GetPool<CharacteristicPreviousValueComponent>();
            _modificationPercentPool = _world.GetPool<PercentModificationsValueComponent>();
            _maxLimitModificationsPool = _world.GetPool<MaxLimitModificationsValueComponent>();
            _valueModificationsPool = _world.GetPool<BaseModificationsValueComponent>();
        }

        public void Run()
        {
            foreach (var request in _requestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(request);

                //is owner alive?
                if (!requestComponent.Owner.Unpack(_world, out var ownerEntity))
                    continue;

                if (_characteristicLinkPool.Has(ownerEntity))
                    continue;

                _ownerChangedPool.GetOrAddComponent(ownerEntity);
                
                var characteristicEntity = _world.NewEntity();
                var characteristicsPacked = _world.PackEntity(characteristicEntity);
                
                ref var characteristicComponent = ref _characteristicPool.Add(characteristicEntity);
                ref var minComponent = ref _minValuePool.Add(characteristicEntity);
                ref var maxComponent = ref _maxValuePool.Add(characteristicEntity);
                ref var baseValueComponent = ref _baseValuePool.Add(characteristicEntity);
                ref var characteristicOwnerComponent = ref _characteristicsOwnerPool.Add(characteristicEntity);
                ref var defaultComponent = ref _defaultValuePool.Add(characteristicEntity);
                ref var previousValue = ref _previousValuePool.Add(characteristicEntity);
                ref var changedComponent = ref _changedPool.Add(characteristicEntity);
                ref var percentModificationsValueComponent = ref _modificationPercentPool.Add(characteristicEntity);
                ref var maxLimitValueComponent = ref _maxLimitModificationsPool.Add(characteristicEntity);
                ref var valueModificationsValueComponent = ref _valueModificationsPool.Add(characteristicEntity);
                
                ownerEntity.AddChild(characteristicEntity, _world);
                
                var maxValue = requestComponent.MaxValue;
                var minValue = requestComponent.MinValue;
                var value = requestComponent.Value;

                maxValue = maxValue <= minValue ? minValue : maxValue;
                minValue = minValue >= maxValue ? maxValue : minValue;

                characteristicComponent.Value =value;
                minComponent.Value = minValue;
                maxComponent.Value = maxValue;
                baseValueComponent.Value = value;
                characteristicOwnerComponent.Link = requestComponent.Owner;
                
                defaultComponent.Value =     value;
                defaultComponent.BaseValue = value;
                defaultComponent.MinValue =  minValue;
                defaultComponent.MaxValue =  maxValue;

                ref var characteristicLinkComponent = ref _characteristicLinkPool.Add(ownerEntity);
                ref var valueComponent = ref _characteristicValuePool.Add(ownerEntity);
                
                characteristicLinkComponent.Value = characteristicsPacked;
                
                valueComponent.Value =     value;
                valueComponent.BaseValue = value;
                valueComponent.MaxValue =  maxValue;
                valueComponent.MinValue =  minValue;
            }
        }
    }
}