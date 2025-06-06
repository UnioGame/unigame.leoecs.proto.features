namespace UniGame.Ecs.Proto.Camera.Systems
{
    using Components;
    using JetBrains.Annotations;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    using UnityEngine;

    /// <summary>
    /// Система отвечающая за слежение камеры за управляемой пользователем сущностью.
    /// </summary>
    [UsedImplicitly]
    public sealed class CameraLookAtTargetSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoIt _targetFilter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CameraLookTargetComponent>().End();
            _targetFilter = _world.Filter<CameraFollowTargetComponent>().End();
        }

        public void Run()
        {
            var targetPool = _world.GetPool<CameraLookTargetComponent>();
            var transformPool = _world.GetPool<TransformComponent>();

            foreach (var entity in _filter)
            {
                ref var targetComponent = ref targetPool.Get(entity);
                ref var transformComponent = ref transformPool.Get(entity);

                Transform targetTransform = null;

                foreach (var targetEntity in _targetFilter)
                {
                    if (!transformPool.Has(targetEntity))
                        continue;

                    ref var targetTransformComponent = ref transformPool.Get(targetEntity);
                    targetTransform = targetTransformComponent.Value;
                    break;
                }

                if (targetTransform == null) continue;

                var transform = transformComponent.Value;

                var currentPosition = transform.position;
                var nextPosition = (float3)targetTransform.position + targetComponent.Offset;

                transform.position = math.lerp(currentPosition, nextPosition, 
                    Time.deltaTime * targetComponent.Speed);
            }
        }
    }
}