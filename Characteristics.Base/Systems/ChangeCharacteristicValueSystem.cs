namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// changed base value of characteristics
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ChangeCharacteristicValueSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private ProtoPool<ChangeCharacteristicRequest> _requestPool;
        private ProtoPool<CharacteristicValueComponent> _characteristicPool;
        private ProtoPool<MinValueComponent> _minPool;
        private ProtoPool<MaxValueComponent> _maxPool;
        
        private ProtoPool<CharacteristicChangedComponent> _changedPool;
        private ProtoPool<CharacteristicPreviousValueComponent> _previousPool;

        private ProtoIt _changeRequestFilter = It
            .Chain<ChangeCharacteristicRequest>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestPool = _world.GetPool<ChangeCharacteristicRequest>();
            _characteristicPool = _world.GetPool<CharacteristicValueComponent>();
            _minPool = _world.GetPool<MinValueComponent>();
            _maxPool = _world.GetPool<MaxValueComponent>();
            _previousPool = _world.GetPool<CharacteristicPreviousValueComponent>();
            
            _changedPool = _world.GetPool<CharacteristicChangedComponent>();
        }

        public void Run()
        {

            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                var value = requestComponent.Value;
                
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_characteristicPool.Has(characteristicEntity))
                    continue;
                
                ref var minComponent = ref _minPool.Get(characteristicEntity);
                ref var maxComponent = ref _maxPool.Get(characteristicEntity);
                ref var valueComponent = ref _characteristicPool.Get(characteristicEntity);
  
                var previousValue = valueComponent.Value;
                var currentValue = valueComponent.Value;
                currentValue += value;
                currentValue = Mathf.Clamp(currentValue, minComponent.Value, maxComponent.Value);
                valueComponent.Value = currentValue;

                //mark as changed
                ref var  eventComponent = ref _changedPool.GetOrAddComponent(characteristicEntity);
                eventComponent.Value = currentValue;
                eventComponent.PreviousValue = previousValue;
            }
            
        }
    }
}