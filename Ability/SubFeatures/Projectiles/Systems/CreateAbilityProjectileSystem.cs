namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Systems
{
    using System;
    using Ability.Aspects;
    using AbilityInventory.Aspects;
    using Aspects;
    using Components;
    using Effects;
    using Effects.Aspects;
    using FakeTimeline.Aspects;
    using FakeTimeline.Components.Requests;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using GameResources.Aspects;
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
    public class CreateAbilityProjectileSystem : IProtoRunSystem
    {
        private GameResourceAspect _gameSpawnAspect;
        private ProjectileAbilityAspect _projectileAbilityAspect;
        private UnityAspect _unityAspect;
        private TimerAspect _timerAspect;
        private AbilityAspect _abilityAspect;
        private AbilityInventoryAspect _abilityInventoryAspect;
        private OwnershipAspect _ownershipAspect;
        private EffectAspect _effectAspect;
        private TimelineAspect _timelineAspect;
        
        private ProtoWorld _world;
        
        private ProtoIt _projectileAbilityFilter = It
            .Chain<ProjectileAbilityComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .End();
        
        public void Run()
        {
            foreach (var playableEntity in _projectileAbilityFilter)
            {
                ref var projectileAbilityComponent = ref _projectileAbilityAspect.ProjectileAbility.Get(playableEntity);
                ref var executeComponent = ref _timelineAspect.TimelineExecute.Get(playableEntity);
                if (!executeComponent.TimelineContextEntity.Unpack(_world, out var contextEntity))
                {
                    continue;
                }

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Get(contextEntity);
                var packedSource = abilityContextComponent.abilityOwner;
                var packedTarget = abilityContextComponent.FirstTarget();
                if (!(packedTarget.Unpack(_world, out var targetEntity) && 
                      packedSource.Unpack(_world, out var sourceEntity)))
                {
                    continue;
                }

                var targetPositionType = projectileAbilityComponent.targetPositionType;
                var targetTransform = targetEntity.GetViewInstance(_world, targetPositionType);
                var targetPosition = (float3)targetTransform.position;
                
                var spawnPositionType = projectileAbilityComponent.spawnPositionType;
                var projectileSpawnTransform = sourceEntity.GetViewInstance(_world, spawnPositionType);
                var projectileSpawnPosition = (float3)projectileSpawnTransform.position;
                
                var projectileAssetGuid = projectileAbilityComponent.assetGuid;
                var lifetime = projectileAbilityComponent.duration;

                var projectileEntity = _gameSpawnAspect.Spawn(default, projectileAssetGuid, projectileSpawnPosition);
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