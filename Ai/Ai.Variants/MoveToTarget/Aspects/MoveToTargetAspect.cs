namespace Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects
{
    using System;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.Ecs.Proto.Gameplay.LevelProgress.Components;
    using UniGame.Ecs.Proto.GameLayers.Layer.Components;
    using UniGame.Ecs.Proto.Characteristics.Radius.Component;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.GameAi.MoveToTarget.Components;
    using UniGame.Ecs.Proto.GameLayers.Category.Components;
    using UniGame.LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MoveToTargetAspect : EcsAspect
    {
        public ProtoPool<MoveByAgroComponent> ByAgro;
        public ProtoPool<MoveByCategoryComponent> ByCategory;
        public ProtoPool<MoveByPoiComponent> ByPoi;
        public ProtoPool<MoveByRangeComponent> ByRange;
        public ProtoPool<MoveFilterData> FilterData;
        public ProtoPool<MoveOutOfRangeComponent> OutOfRange;
        public ProtoPool<MoveToGoalComponent> ToGoal;
        public ProtoPool<MoveToPoiComponent> ToPoi;
        public ProtoPool<MoveToPoiGoalsComponent> ToPoiGoals;
        public ProtoPool<MoveToTargetActionComponent> ToTargetAction;
        public ProtoPool<MoveToTargetPlannerComponent> ToTargetPlanner;
        public ProtoPool<CategoryIdComponent> CategoryId;
        public ProtoPool<LayerIdComponent> Layer;
        public ProtoPool<RadiusComponent> Radius;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<TransformDirectionComponent> Direction;
        public ProtoPool<ActiveGameViewComponent> ActiveView;
    }
}