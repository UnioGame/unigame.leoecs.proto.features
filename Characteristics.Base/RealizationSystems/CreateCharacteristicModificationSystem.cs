namespace UniGame.Ecs.Proto.Characteristics.Base
{
    using System;
    using System.Runtime.CompilerServices;
    using Aspects;
    using Components;
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
    public class CreateCharacteristicModificationSystem<TCharacteristic> : IProtoRunSystem
        where TCharacteristic : struct
    {
        private GameCharacteristicAspect<TCharacteristic> _modificationAspect;
        
        private ProtoIt _createFilter = It
            .Chain<CreateModificationRequest<TCharacteristic>>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _createFilter)
            {
                _modificationAspect.CreateModification.Del(entity);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void AddModification(ProtoWorld world,
            ProtoEntity sourceEntity,
            ProtoEntity destinationEntity, 
            Modification modification)
        {
            if(!world.HasComponent<CharacteristicComponent<TCharacteristic>>(destinationEntity))
                return;
            
            var entity = world.NewEntity();
            ref var request = ref world.AddComponent<AddModificationRequest<TCharacteristic>>(entity);
            
            request.ModificationSource = sourceEntity.PackEntity(world);
            request.Target = destinationEntity.PackEntity(world);
            request.Modification = modification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void RemoveModification(ProtoWorld world,ProtoEntity source, ProtoEntity destinationEntity)
        {
            if(!world.HasComponent<TCharacteristic>(destinationEntity))
                return;
            
            var entity = world.NewEntity();
            ref var removeRequest = ref world
                .AddComponent<RemoveCharacteristicModificationRequest<TCharacteristic>>(entity);
            removeRequest.Source = source.PackEntity(world);
            removeRequest.Target = destinationEntity.PackEntity(world);
        }

    }
}