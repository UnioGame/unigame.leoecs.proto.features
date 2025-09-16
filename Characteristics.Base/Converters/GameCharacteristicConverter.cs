namespace UniGame.Ecs.Proto.Characteristics
{
    using System;
    using System.Threading;
    using Base.Components.Requests;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public abstract class GameCharacteristicConverter<TCharacteristic> : LeoEcsConverter, 
        ICharacteristicConverter
        where TCharacteristic : struct
    {
        public float value;
        public float minValue;
        public float maxValue;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var createCharacteristicRequest = ref world
                .AddComponent<CreateCharacteristicRequest<TCharacteristic>>(entity);
            
            createCharacteristicRequest.Value = value;
            createCharacteristicRequest.MaxValue = maxValue;
            createCharacteristicRequest.MinValue = minValue;
            createCharacteristicRequest.Owner = entity.PackEntity(world);

            OnApply(target,world, entity);
            
            world.AddComponent<TCharacteristic>(entity);
        }

        protected virtual void OnApply(GameObject target, 
            ProtoWorld world, ProtoEntity entity,
            CancellationToken cancellationToken = default)
        {
            
        }
    }
}
