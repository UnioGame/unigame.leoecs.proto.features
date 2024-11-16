namespace UniGame.Ecs.Proto.GameResources.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using Components;
    using Core.Runtime;
    using Data;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
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
        
        //requests
        public ProtoPool<GameResourceSpawnRequest> SpawnRequest;
        public ProtoPool<ResourceInstanceSpawnRequest> InstanceSpawnRequest;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(
            ProtoPackedEntity owner,
            string resourceId,
            float3 spawnPosition,
            Transform parent = null,
            ILifeTime resourceLifeTime = null)
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
                quaternion.identity,
                One,
                parent,
                resourceLifeTime);

            return spawnEntity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Spawn(
            ProtoEntity entity,
            string resourceId, 
            float3 spawnPosition,
            quaternion rotation,
            float3 scale,
            Transform parentTransform = null,
            ILifeTime resourceLifeTime = null)
        {
            ref var spawnRequest = ref SpawnRequest.Add(entity);
            spawnRequest.Parent = parentTransform;
            spawnRequest.ResourceId = resourceId;
            spawnRequest.LifeTime = resourceLifeTime;
            spawnRequest.LocationData = new GamePoint()
            {
                Position = spawnPosition,
                Rotation = rotation,
                Scale = scale
            };
        }
    }
}