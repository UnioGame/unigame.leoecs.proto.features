namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Base;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// reset target characteristic value to default
    [Serializable]
    [ECSDI]
    public class ResetTargetCharacteristicSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private ProtoPool<ResetCharacteristicRequest> _resetPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        private ProtoIt _requestFiler = It
            .Chain<ResetCharacteristicSelfRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _resetPool = _world.GetPool<ResetCharacteristicRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFiler)
            {
                if(!_linkPool.Has(requestEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                
                var entity = _world.NewEntity();
                ref var resetComponent = ref _resetPool.Add(entity);
                resetComponent.Target = linkComponent.Value;
            }
        }
    }
    
    /// <summary>
    /// reset target characteristic value to default
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ResetTargetCharacteristicMaxLimitSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private ProtoPool<ResetCharacteristicMaxLimitSelfRequest> _resetPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;
        
        private ProtoIt _requestFiler = It
            .Chain<ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>>()
            .Inc<CharacteristicComponent<TCharacteristic>>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _resetPool = _world.GetPool<ResetCharacteristicMaxLimitSelfRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFiler)
            {
                if(!_linkPool.Has(requestEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                if(!linkComponent.Value.Unpack(_world,out var characteristicEntity))
                    continue;
                
                _resetPool.GetOrAddComponent(characteristicEntity);
            }
        }
    }
}