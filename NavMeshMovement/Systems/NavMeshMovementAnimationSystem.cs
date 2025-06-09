namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh.Animation
{
    using System;
    using Aspect;
    using Components;
    using Game.Ecs.Core.Components;
    using LeoEcs.Bootstrap;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class NavMeshMovementAnimationSystem : IProtoRunSystem
    {
        private static readonly int ForwardHash = Animator.StringToHash("Forward");
        private static readonly int TurnHash = Animator.StringToHash("Turn");
        
        private ProtoWorld _world;
        private NavMeshAgentAspect _agentAspect;
        private UnityAspect _unityAspect;
        private UnityAnimationAspect _animationAspect;
        private NavMeshMovementAnimationAspect _animationInfoAspect;

        private ProtoIt _filter = It
            .Chain<AnimatorComponent>()
            .Inc<VelocityComponent>()
            .Inc<TransformComponent>()
            .Inc<GroundInfoComponent>()
            .Inc<NavMeshAgentComponent>()
            .Inc<MovementAnimationInfoComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _agentAspect.Velocity.Get(entity);
                var speed = velocityComponent.Value.magnitude;
                
                ref var transform = ref _unityAspect.Transform.Get(entity);
                ref var groundInfo = ref _agentAspect.GroundInfo.Get(entity);
                ref var navMeshAgent = ref _agentAspect.Agent.Get(entity);
                ref var animationInfo = ref _animationInfoAspect.MovementAnimationInfo.Get(entity);
                ref var animatorComponent = ref _animationAspect.Animator.Get(entity);
                
                var animator = animatorComponent.Value;
                var controller = animator.runtimeAnimatorController;
                var agent = navMeshAgent.Value;
                
                if(agent.isOnNavMesh == false) continue;
                
                if (animator == null || !animator.isActiveAndEnabled) continue;
                var velocity = navMeshAgent.Value.velocity.normalized;
                velocity = transform.Value.InverseTransformDirection(velocity);
                velocity = Vector3.ProjectOnPlane(velocity, groundInfo.Normal);
                
                var agentVelocityMagnitude = agent.velocity.magnitude;
                var speedValue = (velocity.z * speed) / animationInfo.RunSpeed;
                var forwardAmount = agentVelocityMagnitude / speed;
                var turnAmount = Mathf.Atan2(velocity.x, velocity.z);

                animator.speed = speedValue > animationInfo.MaxRunSpeed
                    ? speedValue / animationInfo.MaxRunSpeed
                    : 1.0f;
                if(controller == null) continue;
                
                animator.SetFloat(ForwardHash, forwardAmount, 0.1f, Time.deltaTime);
                animator.SetFloat(TurnHash, turnAmount, 0.1f, Time.deltaTime);
            }
        }
    }
}