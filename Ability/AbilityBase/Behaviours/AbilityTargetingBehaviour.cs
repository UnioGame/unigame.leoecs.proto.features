namespace UniGame.Ecs.Proto.Ability.Behaviours
{
    using System;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Relationship;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Radius.Component;
    using UniGame.Ecs.Proto.GameLayers.Category.Components;
    using UniGame.Ecs.Proto.GameLayers.Relationship.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Who the ability can target: radius, relationship (ally / enemy) and category mask.
    /// Target selection (AI or game systems) reads these components from the ability entity.
    /// </summary>
    [Serializable]
    public sealed class AbilityTargetingBehaviour : IAbilityBehaviour
    {
        public float radius = 1.5f;

        [RelationshipIdMask]
        public RelationshipId relationship = (RelationshipId)~0;

        [CategoryIdMask]
        public CategoryId category = (CategoryId)~0;

        public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
        {
            world.GetOrAddComponent<RadiusComponent>(abilityEntity).Value = radius;
            world.GetOrAddComponent<RelationshipIdComponent>(abilityEntity).Value = relationship;
            world.GetOrAddComponent<CategoryIdComponent>(abilityEntity).Value = category;
        }
    }
}