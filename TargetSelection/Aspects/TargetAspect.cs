namespace UniGame.Ecs.Proto.TargetSelection.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;

    [Serializable]
    public class TargetAspect : EcsAspect
    {
        public ProtoPool<KDTreeDataComponent> Data;
        public ProtoPool<KDTreeComponent> Tree;
        public ProtoPool<KDTreeQueryComponent> Query;
        public ProtoPool<TransformComponent> Transform;
        public ProtoPool<TransformPositionComponent> Position;
    }
}