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
    public class ChangeTargetCharacteristicMinLimitationSystem<TCharacteristic> : IProtoInitSystem, IProtoRunSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        
        private ProtoPool<ChangeMinLimitSelfRequest<TCharacteristic>> _requestPool;
        private ProtoPool<CharacteristicLinkComponent<TCharacteristic>> _linkPool;
        private ProtoPool<ChangeMinLimitRequest> _limitPool;
        
        private ProtoIt _changeRequestFilter = It
            .Chain<ChangeMinLimitSelfRequest<TCharacteristic>>()
            .Inc<CharacteristicLinkComponent<TCharacteristic>>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _requestPool = _world.GetPool<ChangeMinLimitSelfRequest<TCharacteristic>>();
            _linkPool = _world.GetPool<CharacteristicLinkComponent<TCharacteristic>>();
            _limitPool = _world.GetPool<ChangeMinLimitRequest>();
        }

        public void Run()
        {
            foreach (var requestEntity in _changeRequestFilter)
            {
                ref var requestComponent = ref _requestPool.Get(requestEntity);
                ref var linkComponent = ref _linkPool.Get(requestEntity);

                var targetRequestEntity = _world.NewEntity();
                ref var targetRequestComponent = ref _limitPool.Add(targetRequestEntity);
                targetRequestComponent.Target = linkComponent.Value;
                targetRequestComponent.Value = requestComponent.Value;
            }
            
        }
    }
}