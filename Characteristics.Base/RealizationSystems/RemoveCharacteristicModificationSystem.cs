namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using Base;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    
    /// <summary>
    /// convert remove modification request to remove characteristic modification request
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveCharacteristicModificationSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private ProtoPool<RemoveCharacteristicModificationRequest<TCharacteristic>> _requestPool;
        private ProtoPool<RemoveModificationRequest> _modificationPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        private ProtoIt _requestFiler = It
            .Chain<RemoveCharacteristicModificationRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestPool = _world.GetPool<RemoveCharacteristicModificationRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _modificationPool = _world.GetPool<RemoveModificationRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFiler)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(characteristicEntity);
                
                var entity = _world.NewEntity();
                ref var removeRequestComponent = ref _modificationPool.Add(entity);
                
                removeRequestComponent.Source = requestComponent.Source;
                removeRequestComponent.Characteristic = linkComponent.Value;
            }
        }
    }
}