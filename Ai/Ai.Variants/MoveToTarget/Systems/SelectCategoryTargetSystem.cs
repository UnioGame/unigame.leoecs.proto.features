namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Ability.Aspects;
    using Characteristics.Radius.Component;
    using Components;
    using Data;
    using Game.Code.GameLayers.Relationship;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Selection;
    using TargetSelection;
    using UniGame.Ecs.Proto.GameLayers.Layer.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
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
    public class SelectCategoryTargetSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityTools;
        private ProtoWorld _world;
        private TargetSelectionSystem _targetSelection;
        
        private ProtoPool<MoveByCategoryComponent> _moveToTargetPlannerPool;
        private ProtoPool<MoveToGoalComponent> _goalPool;
        private ProtoPool<RadiusComponent> _radiusPool;
        private ProtoPool<TransformPositionComponent> _positionPool;
        private ProtoPool<LayerIdComponent> _layerPool;

        private ProtoItExc _filter= It
            .Chain<MoveByCategoryComponent>()
            .Inc<MoveToTargetPlannerComponent>()
            .Inc<MoveToGoalComponent>()
            .Inc<TransformPositionComponent>()
            .Inc<LayerIdComponent>()
            .Exc<DisabledComponent>()
            .End();
        
        private ProtoEntity[] _selection = new ProtoEntity[TargetSelectionData.MaxTargets];

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var plannerComponent = ref _moveToTargetPlannerPool.Get(entity);

                if (!_abilityTools.TryGetInHandAbility(entity, out var ability))
                    continue;

                ref var transformComponent = ref _positionPool.Get(entity);
                ref var layerComponent = ref _layerPool.Get(entity);
           
                var layer = layerComponent.Value;

                var targetEntity = TargetSelectionData.EmptyResult;
                ref var position = ref transformComponent.Position;
                
                var targetPriority = float.MinValue;
                var maxDistancePriority = plannerComponent.MaxPriorityByDistance;
                var distanceToTarget = float.MaxValue;
                var targetPosition = position;
                var minPriority = plannerComponent.MinFilteredTargetPriority;

                var sqrEffectiveDistance = plannerComponent.EffectiveDistance * plannerComponent.EffectiveDistance;
                
                foreach (var category in plannerComponent.FilterData)
                {
                    var categoryId = category.CategoryId;
                    var mask = category.Relationship.GetFilterMask(layer);
                    
                    var count = _targetSelection.SelectEntitiesInArea(
                        _selection, 
                        category.SensorDistance , 
                        ref position, 
                        ref mask,
                        ref categoryId);
                    
                    for (var i = 0; i < count; i++)
                    {
                        var selectionEntity = _selection[i];
                        var priority = minPriority + count - i;

                        ref var selectionTransformComponent = ref _positionPool.Get(selectionEntity);
                        ref var selectionPosition = ref selectionTransformComponent.Position;
                        
                        var distance = math.distancesq(selectionPosition, position);
                        
                        var distancePriority = sqrEffectiveDistance <= 0
                            ? 0
                            : math.lerp(maxDistancePriority, 0, distance / sqrEffectiveDistance);
                        
                        priority += distancePriority;

                        if (priority <= targetPriority) continue;

                        distanceToTarget = distance;
                        targetPriority = priority;
                        targetEntity = selectionEntity;
                        targetPosition = selectionPosition;
                    }
                }

                if (targetEntity.Equals(TargetSelectionData.EmptyResult))
                    continue;

                ref var abilityRadiusComponent = ref _radiusPool.Get(ability);
                var radius = abilityRadiusComponent.Value;
                var complete = distanceToTarget < radius * radius; // distanceToTarget is squared
                targetPriority = complete ? -1 : targetPriority;
            
                if(targetPriority < 0) continue;
                
                ref var component = ref _goalPool.Get(entity);

                var value = new MoveToGoalData()
                {
                    Complete = complete,
                    Position = targetPosition,
                    Priority = targetPriority,
                    Target = targetEntity.PackEntity(_world)
                };
            
                component.Goals.Add(value);
            }
        }
    }
}