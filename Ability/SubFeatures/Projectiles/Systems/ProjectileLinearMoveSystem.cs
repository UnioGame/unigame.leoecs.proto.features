namespace UniGame.Ecs.Proto.Ability.SubFeatures.Projectiles.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Time.Service;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProjectileLinearMoveSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private ProjectileAbilityAspect _projectileAbilityAspect;
        private UnityAspect _unityAspect;

        private ProtoIt _projectileFilter = It
            .Chain<LinearMovementComponent>()
            .Inc<TransformComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var linearMoveEntity in _projectileFilter)
            {
                ref var transformComponent = ref _unityAspect.Transform.Get(linearMoveEntity);
                ref var linearMovementComponent = ref _projectileAbilityAspect.LinearMovement.Get(linearMoveEntity);

                var transform = transformComponent.Value;
                var velocity = linearMovementComponent.velocity;

                var newPosition = transform.position + velocity * GameTime.DeltaTime;
                transform.position = newPosition;
            }
        }
    }
}