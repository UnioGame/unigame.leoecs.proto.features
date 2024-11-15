namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using System.Runtime.CompilerServices;
    using Aspects;
    using Core.Runtime;
    using Data;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class GameSpawnTools : IProtoInitSystem
    {
        public static readonly float3 One = new(1, 1, 1);
        
        private ProtoWorld _world;
        
        private GameResourceAspect _resourceAspect;
        private OwnershipAspect _ownershipAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(
            ProtoPackedEntity owner,
            string resourceId,
            float3 spawnPosition,
            Transform parent = null,
            ILifeTime resourceLifeTime = null)
        {
            var spawnEntity = _world.NewEntity();
            if (owner.Unpack(_world, out var ownerEntity))
            {
                _ownershipAspect.AddChild(ownerEntity, spawnEntity);
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
            ref var spawnRequest = ref _resourceAspect.SpawnRequest.Add(entity);
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