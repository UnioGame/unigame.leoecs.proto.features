namespace UniGame.Ecs.Proto.Movement.Aspect
{
    using System;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class NavMeshAspect : EcsAspect
    {
        //filters
        public ProtoItExc AgentFilter = It
            .Chain<NavMeshAgentComponent>()
            .Exc<DisabledComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();
        
        //components
        public ProtoPool<NavMeshAgentComponent> Agent;
        public ProtoPool<RigidbodyComponent> Rigidbody;
        public ProtoPool<TransformComponent> Transform;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<VelocityComponent> Velocity;
        public ProtoPool<AngularSpeedComponent> RotationSpeed;
        public ProtoPool<ImmobilityComponent> Immobility;
        public ProtoPool<StepMovementComponent> StepMovement;
        public ProtoPool<ComePointComponent> ComePoint;
        public ProtoPool<InstantRotateComponent> InstantRotate;
        public ProtoPool<DisabledComponent> Disabled;

        //requests
        public ProtoPool<MovementStopSelfRequest> NavMeshAgentStop;
        public ProtoPool<MovementPointSelfRequest> MovementTargetPoint;
        public ProtoPool<RotateToPointSelfRequest> RotateTo;
        public ProtoPool<SetNavigationStatusSelfRequest> SetNavigationStatus;
        public ProtoPool<SetKinematicSelfRequest> SetKinematicStatus;
        
    }
}