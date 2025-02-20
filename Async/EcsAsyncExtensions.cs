namespace Game.Modules.leoecs.proto.features.Async
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    public static class EcsAsyncExtensions
    {
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world,
            ProtoEntity entity,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            return await world.WaitComponentAsync<TComponent>(entity.PackEntity(world), cancellationToken);
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world,
            ProtoEntity entity,
            Action<ProtoPackedEntity,ProtoWorld> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            return await world.WaitComponentAsync<TComponent>(entity.PackEntity(world),onComponentAdded, cancellationToken);
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world,
            ProtoEntity entity,
            Func<ProtoPackedEntity,ProtoWorld,UniTask> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            return await world.WaitComponentAsync<TComponent>(entity.PackEntity(world),onComponentAdded, cancellationToken);
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world,
            ProtoEntity entity,
            Func<ProtoPackedEntity,ProtoWorld,UniTask<bool>> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            return await world.WaitComponentAsync<TComponent>(entity.PackEntity(world),onComponentAdded, cancellationToken);
        }

        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world, 
            ProtoPackedEntity entity,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            do
            {
                if(cancellationToken.IsCancellationRequested)
                    return false;
                if(!entity.Unpack(world, out var unpackedEntity))
                    return false;
                if (world.HasComponent<TComponent>(unpackedEntity))
                    return true;
                
                await UniTask.Yield();
                
            } while (true);
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world, 
            ProtoPackedEntity entity,
            Action<ProtoPackedEntity,ProtoWorld> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            var hasComponent = await world.WaitComponentAsync<TComponent>(entity, cancellationToken);
            
            if (hasComponent)
            {
                onComponentAdded(entity, world);
            }
            
            return hasComponent;
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world, 
            ProtoPackedEntity entity,
            Func<ProtoPackedEntity,ProtoWorld,UniTask> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            var hasComponent = await world.WaitComponentAsync<TComponent>(entity, cancellationToken);
            
            if (hasComponent)
                await onComponentAdded(entity, world);
            
            return hasComponent;
        }
        
        public static async UniTask<bool> WaitComponentAsync<TComponent>(this ProtoWorld world, 
            ProtoPackedEntity entity,
            Func<ProtoPackedEntity,ProtoWorld,UniTask<bool>> onComponentAdded,
            CancellationToken cancellationToken = default)
            where TComponent : struct
        {
            var hasComponent = await world.WaitComponentAsync<TComponent>(entity, cancellationToken);
            
            if (hasComponent)
                hasComponent = await onComponentAdded(entity, world);
            
            return hasComponent;
        }

        public static async UniTask<bool> WaitComponentAsync(this ProtoEntity entity,
            ProtoIt it,
            Func<ProtoPackedEntity, ProtoWorld, UniTask> onComponentAdded,
            CancellationToken cancellationToken = default)
        {
            var packedEntity = entity.PackEntity(it.World());
            return await packedEntity.WaitComponentAsync(it, onComponentAdded, cancellationToken);
        }
        
        public static async UniTask<bool> WaitComponentAsync(this ProtoEntity entity,
            ProtoIt it,
            Func<ProtoPackedEntity, ProtoWorld, UniTask<bool>> onComponentAdded,
            CancellationToken cancellationToken = default)
        {
            var packedEntity = entity.PackEntity(it.World());
            return await packedEntity.WaitComponentAsync(it, onComponentAdded, cancellationToken);
        }
        
        public static async UniTask<bool> WaitComponentAsync(this ProtoEntity entity,
            ProtoIt it,
            CancellationToken cancellationToken = default)
        {
            var packedEntity = entity.PackEntity(it.World());
            return await packedEntity.WaitComponentAsync(it, cancellationToken);
        }

        public static async UniTask<bool> WaitComponentAsync(this ProtoPackedEntity entity,
            ProtoIt it,
            Func<ProtoPackedEntity,ProtoWorld,UniTask> onComponentAdded,
            CancellationToken cancellationToken = default)
        {
            var result = await entity.WaitComponentAsync(it, cancellationToken);
            if (!result) return result;
            
            var world = it.World();
            await onComponentAdded(entity, world);

            return result;
        }
        
        public static async UniTask<bool> WaitComponentAsync(this ProtoPackedEntity entity,
            ProtoIt it,
            Func<ProtoPackedEntity,ProtoWorld,UniTask<bool>> onComponentAdded,
            CancellationToken cancellationToken = default)
        {
            var result = await entity.WaitComponentAsync(it, cancellationToken);
            if (!result) return result;
            
            var world = it.World();
            result = await onComponentAdded(entity, world);

            return result;
        }
        
        public static async UniTask<bool> WaitComponentAsync(this ProtoPackedEntity entity,
            ProtoIt it,
            Action<ProtoPackedEntity,ProtoWorld> onComponentAdded,
            CancellationToken cancellationToken = default)
        {
            var result = await entity.WaitComponentAsync(it, cancellationToken);
            if (!result) return result;
            
            var world = it.World();
            onComponentAdded(entity, world);

            return result;
        }

        public static async UniTask<bool> WaitComponentAsync(this ProtoPackedEntity entity,
            ProtoIt it,
            CancellationToken cancellationToken = default)
        {
            var world = it.World();
            do
            {
                if(cancellationToken.IsCancellationRequested)
                    return false;
                if(!entity.Unpack(world, out var unpackedEntity))
                    return false;
                if (it.Has(unpackedEntity))
                    return true;
                
                await UniTask.Yield();
                
            } while (true);
        }
    }
}