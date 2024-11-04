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
        public static ProtoPackedEntity EmptyEntity = default;
        
        private ProtoWorld _world;
        
        private GameResourceAspect _resourceAspect;
        private GameResourceTaskAspect _taskAspect;
        private OwnershipAspect _ownershipAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(string resourceId, 
            float3 spawnPosition,
            Transform parent = null)
        {
            return Spawn(ref EmptyEntity, resourceId, spawnPosition, parent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(
            ref ProtoPackedEntity owner,
            string resourceId, 
            float3 spawnPosition,
            Transform parent = null)
        {
            return Spawn(ref owner,ref EmptyEntity, resourceId, spawnPosition, parent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity Spawn(
            ref ProtoPackedEntity owner,
            ref ProtoPackedEntity source,
            string resourceId, 
            float3 spawnPosition,
            Transform parent = null)
        {
            var spawnEntity = _world.NewEntity();

            if (owner != EmptyEntity)
            {
                _ownershipAspect.AddChild(ref owner, ref spawnEntity);
            }

            var spawnPacked = _world.PackEntity(spawnEntity);
            ref var resourceIdComponent = ref _resourceAspect.Resource.Add(spawnEntity);
            resourceIdComponent.Value = resourceId;

            ref var lifetimeComponent = ref _ownershipAspect.LifeTime.Add(spawnEntity);
            
            Spawn(ref source,
                ref spawnPacked,
                ref EmptyEntity,
                resourceId,
                spawnPosition,
                quaternion.identity,
                One,
                parent,
                lifetimeComponent);

            return spawnEntity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Spawn(
            ref ProtoPackedEntity source,
            ref ProtoPackedEntity target,
            ref ProtoPackedEntity parent,
            string resourceId, 
            float3 spawnPosition,
            quaternion rotation,
            float3 scale,
            Transform parentTransform = null,
            ILifeTime lifeTime = null)
        {
            var spawnEntity = _world.NewEntity();
            ref var spawnRequest = ref _resourceAspect.Spawn.Add(spawnEntity);
            
            spawnRequest.Source = source;
            spawnRequest.Target = target;
            spawnRequest.Parent = parentTransform;
            spawnRequest.ParentEntity = parent;
            spawnRequest.ResourceId = resourceId;
            spawnRequest.LifeTime = lifeTime;
            spawnRequest.LocationData = new GamePoint()
            {
                Position = spawnPosition,
                Rotation = rotation,
                Scale = scale
            };
        }
    }
}