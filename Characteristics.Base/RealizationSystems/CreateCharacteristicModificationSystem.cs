namespace UniGame.Ecs.Proto.Characteristics.Base
{
    using System;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateCharacteristicModificationSystem<TCharacteristic> : IProtoRunSystem,IProtoInitSystem
        where TCharacteristic : struct
    {
        private ProtoWorld _world;
        private ProtoPool<CreateModificationRequest<TCharacteristic>> _createModificationPool;
        
        private ProtoIt _createFilter = It
            .Chain<CreateModificationRequest<TCharacteristic>>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _createModificationPool = _world.GetPool<CreateModificationRequest<TCharacteristic>>();
        }
        
        public void Run()
        {
            foreach (var entity in _createFilter)
            {
                _createModificationPool.Del(entity);
            }
        }


    }
}