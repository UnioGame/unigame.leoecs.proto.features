namespace UniGame.Ecs.Proto.Characteristics.Base.RealizationSystems
{
    using System;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RecalculateCharacteristicSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private ProtoPool<RecalculateCharacteristicSelfRequest<TCharacteristic>> _requestPool;
        private ProtoPool<RecalculateCharacteristicSelfRequest> _recalculatePool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        private ProtoIt _requestFilter = It
            .Chain<RecalculateCharacteristicSelfRequest<TCharacteristic>>()
            .Inc<CharacteristicLinkComponent<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _recalculatePool = _world.GetPool<RecalculateCharacteristicSelfRequest>();
            _requestPool = _world.GetPool<RecalculateCharacteristicSelfRequest<TCharacteristic>>();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                
                if(!linkComponent.Value.Unpack(_world,out var linkEntity))
                    continue;

                _recalculatePool.GetOrAddComponent(linkEntity);
                _requestPool.Del(requestEntity);
            }
        }
    }
}