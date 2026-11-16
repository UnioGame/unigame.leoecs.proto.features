namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система отвечающая за перемещение в целевую позицю при наличии компонента <see
    /// cref="MovementPointSelfRequest"/>.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class NavMeshTargetMovementSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _navigationAspect;
        
        private ProtoItExc _filter = It
            .Chain<MovementPointSelfRequest>()
            .Inc<NavMeshAgentComponent>()
            .Inc<AngularSpeedComponent>()
            .Inc<NavAgentSpeedComponent>()
            .Inc<InstantRotateComponent>()
            .Exc<ImmobilityComponent>()
            .Exc<DisabledComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                ref var movementTargetPoint = ref _navigationAspect.MovementTargetPoint.Get(entity);
                ref var speedComponent = ref _navigationAspect.AgentSpeed.Get(entity);
                ref var angularSpeedComponent = ref _navigationAspect.RotationSpeed.Get(entity);
                ref var instantRotateComponent = ref _navigationAspect.InstantRotate.Get(entity);
                
                var navMeshAgent = navMeshAgentComponent.Value;
                if(!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh)
                    continue;
                
                if (instantRotateComponent.Value)
                {
                    var transform = navMeshAgent.transform;
                    var position = movementTargetPoint.Value;
                    position.y = transform.position.y;
                    transform.LookAt(position);
                }
                
                navMeshAgent.speed = speedComponent.Value;
                navMeshAgent.angularSpeed = angularSpeedComponent.Value;

                navMeshAgent.destination = movementTargetPoint.Value;

                if (navMeshAgent.isStopped)
                    navMeshAgent.isStopped = false;
            }
        }
    }
}