namespace UniGame.Ecs.Proto.Movement.Aspect
{
    using System;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class NavMeshAgentAspect : EcsAspect
    {
        //filters
        public ProtoIt NavMeshAgentFilter = It
            .Chain<NavMeshAgentComponent>()
            .End();
        
        public ProtoIt ImmobilizedAgentFilter = It
            .Chain<NavMeshAgentComponent>()
            .Inc<ImmobilityComponent>()
            .End();
        
        public ProtoIt DisabledAgentFilter = It
            .Chain<NavMeshAgentComponent>()
            .Inc<DisabledComponent>()
            .End();
        
        //components
        public ProtoPool<NavMeshAgentComponent> Agent;
        public ProtoPool<RigidbodyComponent> Rigidbody;
        public ProtoPool<TransformComponent> Transform;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<VelocityComponent> Velocity;
        public ProtoPool<GroundInfoComponent> GroundInfo;
        public ProtoPool<AngularSpeedComponent> RotationSpeed;
        public ProtoPool<DisabledComponent> Disabled;
        public ProtoPool<RotationComponent> Rotation;
        public ProtoPool<NavAgentSpeedComponent> AgentSpeed;
        
        public ProtoPool<ImmobilityComponent> Immobility;
        public ProtoPool<StepMovementComponent> StepMovement;
        
        public ProtoPool<ComePointComponent> ComePoint;
        public ProtoPool<InstantRotateComponent> InstantRotate;

        //requests
        public ProtoPool<MovementPointSelfRequest> MovementTargetPoint;
        public ProtoPool<MovementStopSelfRequest> NavMeshAgentStop;
        public ProtoPool<RotateToPointSelfRequest> RotateToPoint;
        public ProtoPool<SetNavAgentVelocitySelfRequest> SetNavAgentVelocity;
        public ProtoPool<SetNavigationStatusSelfRequest> SetNavigationStatus;
        public ProtoPool<SetKinematicSelfRequest> SetKinematicStatus;
        
    }
}