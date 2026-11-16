namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Movement.Aspect;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using Ability.Aspects;
    using AI.Abstract;
    using AI.Components;
    using Components;
    using Effects;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Movement.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

    /// <summary>
    /// Действие ИИ «идти к цели»: разворачивает агента к цели и отправляет NavMeshAgent туда. У цели (ближе
    /// 0.5 м) останавливает.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MoveToTargetAiSystem : IAiActionSystem
    {
        // Stop distance to the goal point (squared). Must be below the attack radius, otherwise a melee unit stops out of reach.
        public float minSqrDistance = 0.25f;
        
        private ProtoWorld _world;
        private AbilityAspect _abilityTools;
        
        private MoveToTargetAspect _moveToTargetAspect;
        private NavMeshAgentAspect _navMeshAgentAspect;

        private ProtoItExc _filter = It
            .Chain<MoveToTargetActionComponent>()
            .Inc<TransformPositionComponent>()
            .Inc<TransformDirectionComponent>()
            .Inc<AiAgentComponent>()
            .Exc<ImmobilityComponent>()
            .Exc<DisabledComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var moveToTargetComponent = ref _moveToTargetAspect.ToTargetAction.Get(entity);
                ref var directionComponent = ref _moveToTargetAspect.Direction.Get(entity);
                ref var transformComponent = ref _moveToTargetAspect.Position.Get(entity);

                var position = transformComponent.Position;
                var targetPosition = moveToTargetComponent.Position;
                var sqrDistance = math.distancesq(targetPosition,position);
                
                ref var destination = ref moveToTargetComponent.Position;
                ref var rotateToPoint = ref _navMeshAgentAspect.RotateToPoint.GetOrAddComponent(entity);
                rotateToPoint.Point = destination;
                
                if(_moveToTargetAspect.ActiveView.Has(entity))
                    rotateToPoint.Point = directionComponent.Forward + position;
                
                if (sqrDistance < minSqrDistance)
                {
                    _navMeshAgentAspect.NavMeshAgentStop.GetOrAddComponent(entity);
                    continue;
                }

                // only after the stop check: an added request with default Value would send the unit to (0, 0, 0)
                ref var targetComponentRequest = ref _navMeshAgentAspect.MovementTargetPoint.GetOrAddComponent(entity);
                targetComponentRequest.Value = destination;
                var packedEntity = _world.PackEntity(entity);
                moveToTargetComponent.Effects.CreateRequests(_world,packedEntity,packedEntity);
            }
        }
    }
}
