namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Systems
{
    using System;
    using Ability.Aspects;
    using AbilityInventory.Aspects;
    using Aspects;
    using Common.Components;
    using Components;
    using Effects;
    using Effects.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using GameResources.Systems;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
    public class CreateAbilityProjectileSystem : IProtoInitSystem, IProtoRunSystem
    {
        private GameSpawnTools _gameSpawnTools;
        
        private ProtoWorld _world;
        
        private ProjectileAbilityAspect _projectileAbilityAspect;
        private UnityAspect _unityAspect;
        private TimerAspect _timerAspect;
        private AbilityAspect _abilityAspect;
        private AbilityInventoryAspect _abilityInventoryAspect;
        private OwnershipAspect _ownershipAspect;
        private EffectAspect _effectAspect;
        
        private ProtoIt _projectileAbilityFilter = It
            .Chain<AbilityStartUsingSelfEvent>()
            .Inc<ProjectileAbilityComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _gameSpawnTools = _world.GetGlobal<GameSpawnTools>();
        }

        public void Run()
        {
            foreach (var abilityEntity in _projectileAbilityFilter)
            {
                ref var projectileAbilityComponent = ref _projectileAbilityAspect.ProjectileAbility.Get(abilityEntity);
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityEntity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var unpackedOwnerEntity))
                {
                    continue;
                }
                
                ref var targetsComponent = ref _abilityAspect.Targets.Get(unpackedOwnerEntity);
                var target = targetsComponent.Entities[0];
                if (!target.Unpack(_world, out var unpackedTarget))
                {
                    continue;
                }

                var targetPositionType = projectileAbilityComponent.targetPositionType;
                var targetTransform = unpackedTarget.GetViewInstance(_world, targetPositionType);
                var targetPosition = (float3)targetTransform.position;
                
                var spawnPositionType = projectileAbilityComponent.spawnPositionType;
                var projectileSpawnTransform = unpackedOwnerEntity.GetViewInstance(_world, spawnPositionType);
                var projectileSpawnPosition = (float3)projectileSpawnTransform.position;
                
                var projectileAssetGuid = projectileAbilityComponent.assetGuid;
                var lifetime = projectileAbilityComponent.duration;

                var projectileEntity = _gameSpawnTools.Spawn(projectileAssetGuid, projectileSpawnPosition);
                _projectileAbilityAspect.Projectile.Add(projectileEntity);

                ref var cooldownComponent = ref _timerAspect.Cooldown.Add(projectileEntity);
                cooldownComponent.Value = lifetime;
                _timerAspect.Restart.Add(projectileEntity);
                
                ref var linearMovementComponent = ref _projectileAbilityAspect.LinearMovement.Add(projectileEntity);
                linearMovementComponent.velocity = (targetPosition - projectileSpawnPosition) / lifetime;
            }
        }
    }
}