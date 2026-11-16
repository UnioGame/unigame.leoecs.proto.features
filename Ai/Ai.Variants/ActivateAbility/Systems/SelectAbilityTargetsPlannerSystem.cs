namespace UniGame.Ecs.Proto.GameAi.ActivateAbility
{
    using System;
    using System.Runtime.CompilerServices;
    using Ability.Aspects;
    using AI.Components;
    using Components;
    using Game.Code.Ai.ActivateAbility;
    using Game.Code.Ai.ActivateAbility.Aspects;
    using Game.Code.GameLayers.Relationship;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Selection;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

    /// <summary>
    /// Ищет цель для способности «бей при первой возможности»: перебирает слоты агента и берёт первого
    /// подходящего врага в радиусе способности.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SelectAbilityTargetsPlannerSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityTools;
        //private AbilityTargetTools _targetTools;
        
        private ProtoWorld _world;
        private AbilityAiActionAspect _targetAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private TargetSelectionSystem _targetSelection;
        
        private ProtoEntity[] _resultSelection = new ProtoEntity[TargetSelectionData.MaxTargets];

        private ProtoItExc _filter= It
            .Chain<AiAgentComponent>()
            .Inc<AbilityByDefaultComponent>()
            .Inc<TransformPositionComponent>()
            .Inc<LayerIdComponent>()
            .Inc<ActivateAbilityPlannerComponent>()
            .Exc<AbilityAiActionTargetComponent>()
            .Exc<DisabledComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                var abilityInUse = _abilityTools.GetNonDefaultAbilityInUse(entity);
                if(abilityInUse.Unpack(_world,out var abilityInUseEntity)) continue;
                
                ref var defaultAbilityComponent = ref _targetAspect.DefaultAbility.Get(entity);
                var abilityFilters = defaultAbilityComponent.FilterData;
                var targetFound = false;

                for (var index = 0; index < abilityFilters.Length; index++)
                {
                    if(targetFound) break;
                    
                    ref var abilityFilter = ref defaultAbilityComponent.FilterData[index];
                    var slot = abilityFilter.Slot;

                    if (_abilityOwnerAspect.ApplyAbilityBySlot.Has(entity))
                    {
                        ref var slotRequest = ref _abilityOwnerAspect.ApplyAbilityBySlot.Get(entity);
                        slot = slotRequest.AbilitySlot;
                    }

                    var abilityEntity = _abilityTools.TryGetAbility(entity, slot);
                    if ((int)abilityEntity < 0) continue;
                    
                    var abilityTargetEntity = SelectAbilityTarget(entity, ref abilityEntity, ref abilityFilter);
                    
                    if ((int)abilityTargetEntity < 0)
                    {
                        //_targetTools.ClearAbilityTargets(abilityEntity);
                        continue;
                    }

                    var packedTarget = _world.PackEntity(abilityTargetEntity);
   
                   // _targetTools.SetAbilityTarget(abilityEntity,packedTarget,slot);
                    
                    var cooldownPassed = _abilityTools.IsAbilityCooldownPassed(abilityEntity);
                    
                    //проверяем кулдаун абилки, если он не прошел - игнорируем
                    if (targetFound || !cooldownPassed) continue;
                    
                    ref var activeTargetComponent = ref _targetAspect.ActiveTarget.GetOrAddComponent(abilityEntity);
                    activeTargetComponent.Value = packedTarget;
                    
                    targetFound = true;
                    ref var targetComponent = ref _targetAspect.AiTarget.Add(entity);
                        
                    targetComponent.Ability = _world.PackEntity(abilityEntity);
                    targetComponent.AbilityCellId = slot;
                    targetComponent.AbilityTarget = packedTarget;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ProtoEntity SelectAbilityTarget(ProtoEntity entity, ref ProtoEntity ability, ref AbilityFilter filter)
        {
            ref var activeTargetComponent = ref _targetAspect.ActiveTarget.GetOrAddComponent(ability);
            ref var radiusComponent = ref _targetAspect.Radius.Get(ability);
            ref var filterComponent = ref _targetAspect.Relationship.Get(ability);
            ref var categoryIdComponent = ref _targetAspect.Category.Get(ability);
            
            ref var gameLayerComponent = ref _targetAspect.LayerId.Get(entity);
            ref var positionComponent = ref _targetAspect.Position.Get(entity);
            
            var entityPosition = positionComponent.Position;
            
            //create relation mask by gamelayer id
            var gameLayerMask = filterComponent.Value.GetFilterMask(gameLayerComponent.Value);

            var amount = _targetSelection.SelectEntitiesInArea(
                _resultSelection,
                radiusComponent.Value,
                ref entityPosition,
                ref gameLayerMask,
                ref categoryIdComponent.Value);

            var target = TargetSelectionData.EmptyResult;
            var maxPriority = float.MinValue;
            var distanceToTarget = float.MaxValue;

            for (var j = 0; j < amount; j++)
            {
                var selectionEntity = _resultSelection[j];
                
                ref var selectionCategoryComponent = ref _targetAspect.Category.Get(selectionEntity);
                ref var transformComponent = ref _targetAspect.Position.Get(selectionEntity);

                ref var position = ref transformComponent.Position;
                // Priorities is not serialized when UsePriority is off, so it can be null
                var count = filter.UsePriority && filter.Priorities != null ? filter.Priorities.Length : 0;
                var priorities = filter.Priorities;
                var priority = 0f;

                for (var i = 0; i < count && filter.UsePriority; i++)
                {
                    var categoryValue = priorities[i];
                    var category = categoryValue.Category;
                    if ((category & selectionCategoryComponent.Value) == 0)
                        continue;

                    var mult = (count - i);
                    priority += mult;
                }
  
                if (priority < maxPriority) continue;

                var distance = math.distancesq(position, entityPosition);
                
                // equal priority: the nearer target wins
                if (priority <= maxPriority && distance >= distanceToTarget) continue;

                maxPriority = priority;
                distanceToTarget = distance;
                target = selectionEntity;
            }
            
            return target.Equals(TargetSelectionData.EmptyResult) 
                ? TargetSelectionData.EmptyResult 
                : target;
        }
    }
}