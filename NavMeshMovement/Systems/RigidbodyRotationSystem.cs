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
    /// Разворачивает Rigidbody сущности в сторону движения.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RigidbodyRotationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _agentAspect;
        
        private ProtoIt _filter = It
            .Chain<RotationComponent>()
            .Inc<RigidbodyComponent>()
            .Inc<AngularSpeedComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var quaternionComponent = ref _agentAspect.Rotation.Get(entity);
                ref var rigidbodyComponent = ref _agentAspect.Rigidbody.Get(entity);
                ref var rotationSpeedComponent = ref _agentAspect.RotationSpeed.Get(entity);

                var rigidbody = rigidbodyComponent.Value;
                
                var previous = rigidbody.rotation;
                var current = quaternionComponent.Value;

                var rotationSpeed = rotationSpeedComponent.Value;
                var angle = Quaternion.Angle(previous, current);
                var rotationTime = rotationSpeed / angle;

                var lerpRotation = Quaternion.Lerp(previous, current, rotationTime * Time.deltaTime);
                
                rigidbody.MoveRotation(lerpRotation);
            }
        }
    }
}