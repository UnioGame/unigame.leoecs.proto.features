namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Behaviours
{
    using System;
    using Components;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Configuration.Runtime.Effects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public sealed class VisualsBehaviour : IAbilityBehaviour
    {
        public AssetReferenceT<GameObject> visualsPrefab;
        public ViewInstanceType spawnViewInstance;
        public float spawnDelay;
        public bool targetSource;
        public bool boneBound;
        
        public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
        {
            ref var abilityVisualsComponent = ref world.AddComponent<AbilityVisualsComponent>(abilityEntity);
            abilityVisualsComponent.spawnPosition = (int)spawnViewInstance;
            abilityVisualsComponent.assetIdentification = visualsPrefab.AssetGUID;
            abilityVisualsComponent.spawnDelay = spawnDelay;
            abilityVisualsComponent.targetSource = targetSource;
            abilityVisualsComponent.boneBound = boneBound;
        }
    }
}