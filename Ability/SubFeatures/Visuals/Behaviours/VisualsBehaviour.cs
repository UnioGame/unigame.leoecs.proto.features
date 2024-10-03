namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Behaviours
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
    public sealed class VisualsBehaviour : TimelineAbilityBehaviour
    {
        public AssetReferenceT<GameObject> visualsPrefab;
        public ViewInstanceType spawnViewInstance;
        public bool boneBound;

        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            base.ComposeBehaviour(world, abilityEntity, playableEntity);
            
            ref var abilityVisualsComponent = ref world.AddComponent<AbilityVisualsPlayableComponent>(playableEntity);
            abilityVisualsComponent.spawnPosition = (int)spawnViewInstance;
            abilityVisualsComponent.assetIdentification = visualsPrefab.AssetGUID;
            abilityVisualsComponent.boneBound = boneBound;
        }
    }
}