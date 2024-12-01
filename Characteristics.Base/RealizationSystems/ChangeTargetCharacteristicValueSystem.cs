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
    public class ChangeTargetCharacteristicValueSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        
        private ProtoPool<ChangeCharacteristicValueRequest<TCharacteristic>> _requestPool;
        private ProtoPool<ChangeCharacteristicRequest> _changePool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        private ProtoIt _changeRequestFilter = It
            .Chain<ChangeCharacteristicValueRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestPool = _world.GetPool<ChangeCharacteristicValueRequest<TCharacteristic>>();
            _changePool = _world.GetPool<ChangeCharacteristicRequest>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
        }

        public void Run()
        {
            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                if(!requestComponent.Target.Unpack(_world,out var characteristicEntity))
                    continue;
                
                if(!_linkPool.Has(characteristicEntity)) continue;

                ref var linkComponent = ref _linkPool.Get(characteristicEntity);

                var targetEntity = _world.NewEntity();
                ref var targetRequest = ref _changePool.Add(targetEntity);
                targetRequest.Target = linkComponent.Value;
                targetRequest.Source = requestComponent.Source;
                targetRequest.Value = requestComponent.Value;
            }
            
        }
    }
}