namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Behaviours
{
    using System;
    using Components;
    using FakeTimeline.Data;
    using Game.Code.Configuration.Runtime.Effects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public sealed class ProjectileBehaviour : TimelineAbilityBehaviour
    {
        public ViewInstanceType spawnViewInstance;
        public ViewInstanceType targetViewInstance;
        
        public AssetReferenceT<GameObject> projectilePrefab;
        public float duration;
        public int trajectoryType;
        
        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            ref var projectileAbilityComponent = ref world.AddComponent<ProjectileAbilityComponent>(playableEntity);
            projectileAbilityComponent.spawnPositionType = (int)spawnViewInstance;
            projectileAbilityComponent.targetPositionType = (int)targetViewInstance;
            projectileAbilityComponent.assetGuid = projectilePrefab.AssetGUID;
            projectileAbilityComponent.duration = duration;
            projectileAbilityComponent.trajectoryType = trajectoryType;
        }
    }
}