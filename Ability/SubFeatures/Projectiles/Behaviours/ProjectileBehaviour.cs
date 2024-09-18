namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Behaviours
{
    using System;
    using Components;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Configuration.Runtime.Effects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class ProjectileBehaviour : IAbilityBehaviour
    {
        public ViewInstanceType spawnViewInstance;
        public ViewInstanceType targetViewInstance;
        
        public AssetReferenceT<GameObject> projectilePrefab;
        public float duration;
        public int trajectoryType;
        
        public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
        {
            ref var projectileAbilityComponent = ref world.AddComponent<ProjectileAbilityComponent>(abilityEntity);
            projectileAbilityComponent.spawnPositionType = (int)spawnViewInstance;
            projectileAbilityComponent.targetPositionType = (int)targetViewInstance;
            projectileAbilityComponent.assetGuid = projectilePrefab.AssetGUID;
            projectileAbilityComponent.duration = duration;
            projectileAbilityComponent.trajectoryType = trajectoryType;
        }
    }
}