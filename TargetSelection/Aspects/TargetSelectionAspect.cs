namespace UniGame.Ecs.Proto.TargetSelection.Aspects
{
    using System;
    using Characteristics.Radius.Component;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using GameLayers.Relationship.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class TargetSelectionAspect : EcsAspect
    {
        public ProtoPool<SqrRangeFilterTargetComponent> Target;
        public ProtoPool<SqrRangeTargetSelectionComponent> Data;
        public ProtoPool<SqrRangeTargetsResultComponent> Result;
        public ProtoPool<LayerIdComponent> Layer;
        public ProtoPool<CategoryIdComponent> Category;
        public ProtoPool<TransformComponent> Transform;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<EntityAvatarComponent> Avatar;
        public ProtoPool<KDTreeDataComponent> KDData;
        public ProtoPool<DisabledComponent> Disabled;
        
        public ProtoPool<RadiusComponent> Radius;
        public ProtoPool<RelationshipIdComponent> Relationship;
    }
}