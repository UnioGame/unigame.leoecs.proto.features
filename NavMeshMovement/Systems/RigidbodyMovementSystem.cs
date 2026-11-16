namespace UniGame.Ecs.Proto.Movement.Systems.Rigidbody
{
    using System;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Aspect;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Components;
    using UnityEngine;

    /// <summary>
    /// Двигает сущность через Rigidbody со скоростью из VelocityComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RigidbodyMovementSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _agentAspect;

        private ProtoIt _filter = It
            .Chain<VelocityComponent>()
            .Inc<RigidbodyComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _agentAspect.Velocity.Get(entity);
                ref var rigidbodyComponent = ref _agentAspect.Rigidbody.Get(entity);
                ref var speedComponent = ref _agentAspect.AgentSpeed.Get(entity);

                var rigidbody = rigidbodyComponent.Value;

                var currentPosition = rigidbody.position;
                var nextPosition = currentPosition + velocityComponent.Value * speedComponent.Value * Time.deltaTime;
                rigidbody.MovePosition(nextPosition);
            }
        }
    }
}