namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using Ability.Aspects;
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

    /// <summary>
    /// Ищет врагов нужных категорий в поле зрения агента и добавляет лучшую цель в список целей движения.
    /// Ближние цели важнее дальних.
    /// </summary>
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
        
        private MoveToTargetAspect _moveToTargetAspect;

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
                ref var plannerComponent = ref _moveToTargetAspect.ByCategory.Get(entity);

                if (!_abilityTools.TryGetInHandAbility(entity, out var ability))
                    continue;

                ref var transformComponent = ref _moveToTargetAspect.Position.Get(entity);
                ref var layerComponent = ref _moveToTargetAspect.Layer.Get(entity);
           
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

                        ref var selectionTransformComponent = ref _moveToTargetAspect.Position.Get(selectionEntity);
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

                ref var abilityRadiusComponent = ref _moveToTargetAspect.Radius.Get(ability);
                var radius = abilityRadiusComponent.Value;
                var complete = distanceToTarget < radius * radius; // distanceToTarget is squared
                targetPriority = complete ? -1 : targetPriority;
            
                if(targetPriority < 0) continue;
                
                ref var component = ref _moveToTargetAspect.ToGoal.Get(entity);

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