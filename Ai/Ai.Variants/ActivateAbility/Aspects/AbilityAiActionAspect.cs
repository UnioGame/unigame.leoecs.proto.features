namespace Game.Code.Ai.ActivateAbility.Aspects
{
    using System;
    using Ecs.Core.Components;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Ability.Common.Components;
    using UniGame.Ecs.Proto.Characteristics.Radius.Component;
    using UniGame.Ecs.Proto.GameAi.ActivateAbility;
    using UniGame.Ecs.Proto.GameAi.ActivateAbility.Components;
    using UniGame.Ecs.Proto.GameLayers.Category.Components;
    using UniGame.Ecs.Proto.GameLayers.Layer.Components;
    using UniGame.Ecs.Proto.GameLayers.Relationship.Components;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap;

    [Serializable]
    public class AbilityAiActionAspect : EcsAspect
    {
        public ProtoPool<RadiusComponent> Radius;
        public ProtoPool<RelationshipIdComponent> Relationship;
        public ProtoPool<CategoryIdComponent> Category;
        public ProtoPool<TransformComponent> Transform;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<TransformDirectionComponent> Direction;
        public ProtoPool<AbilityActionActiveTargetComponent> ActiveTarget;
        public ProtoPool<LayerIdComponent> LayerId;
        public ProtoPool<EntityAvatarComponent> Avatar;
        public ProtoPool<AbilityAiActionTargetComponent> AiTarget;
        public ProtoPool<AbilityByDefaultComponent> DefaultAbility;
        public ProtoPool<AbilityByRangeComponent> ByRangeAbility;
        public ProtoPool<AbilityRangeActionDataComponent> AbilityRangeData;
        
        //request
        public ProtoPool<ApplyAbilityBySlotSelfRequest> ApplyAbilityFromSlot;
    }
}