namespace UniGame.Ecs.Proto.GameResources.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using Components;
    using Core.Runtime;
    using Data;
    using UniGame.Proto.Ownership;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class GameResourceAspect : EcsAspect
    {
        public static readonly float3 One = new(1, 1, 1);
        
        public OwnershipAspect OwnershipAspect;
        
        public ProtoPool<GameResourceSpawnComponent> SpawnResource;
        public ProtoPool<GameResourceLoadTaskComponent> LoadTask;
        public ProtoPool<PoolableComponent> Poolable;
        public ProtoPool<AddressablesGuidComponent> AddressablesGuid;
        
        //requests
        public ProtoPool<GameResourceSpawnRequest> SpawnRequest;
        public ProtoPool<ResourceInstanceSpawnRequest> InstanceSpawnRequest;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(
            ProtoPackedEntity owner,
            string resourceId,
            float3 spawnPosition,
            Quaternion spawnRotation = default,
            Transform parent = null,
            ILifeTime resourceLifeTime = null,
            bool setActive = false)
        {
            var spawnEntity = world.NewEntity();
            if (owner.Unpack(world, out var ownerEntity))
            {
                OwnershipAspect.AddChild(ownerEntity, spawnEntity);
            }
            
            Spawn(
                spawnEntity,
                resourceId,
                spawnPosition,
                spawnRotation,
                One,
                parent,
                resourceLifeTime,
                setActive);

            return spawnEntity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Spawn(
            ProtoEntity entity,
            string resourceId, 
            float3 spawnPosition,
            Quaternion rotation,
            float3 scale,
            Transform parentTransform = null,
            ILifeTime resourceLifeTime = null,
            bool setActive = false)
        {
            ref var spawnRequest = ref SpawnRequest.Add(entity);
            spawnRequest.Parent = parentTransform;
            spawnRequest.ResourceId = resourceId;
            spawnRequest.LifeTime = resourceLifeTime;
            spawnRequest.SetActive = setActive;
            spawnRequest.LocationData = new GamePoint()
            {
                Position = spawnPosition,
                Rotation = rotation,
                Scale = scale
            };
        }
    }
}