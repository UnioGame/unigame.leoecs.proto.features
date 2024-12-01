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
    /// reset target characteristic value to default
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ResetTargetCharacteristicModificationsSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        
        private ProtoPool<ResetModificationsRequest> _resetPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;

        private ProtoIt _requestFiler = It
            .Chain<ResetCharacteristicModificationsSelfRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _resetPool = _world.GetPool<ResetModificationsRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _requestFiler)
            {
                if (!_linkPool.Has(requestEntity))
                    continue;
                
                ref var linkComponent = ref _linkPool.Get(requestEntity);
                
                var entity = _world.NewEntity();
                ref var resetComponent = ref _resetPool.Add(entity);
                resetComponent.Characteristic = linkComponent.Value;
            }
        }
    }
}