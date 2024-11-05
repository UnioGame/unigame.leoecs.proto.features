namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public class CreateSpawnObjectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameResourceTaskAspect _taskAspect;
        private GameResourceAspect _resourceAspect;

        private ProtoItExc _filter = It
            .Chain<GameResourceResultComponent>()
            .Inc<GameResourceTaskComponent>()
            .Exc<GameResourceTaskCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                _taskAspect.Complete.Add(entity);

                ref var resourceComponent = ref _taskAspect.Result.Get(entity);
                var resource = resourceComponent.Resource as Object;
                var lifeTime = resourceComponent.LifeTime;
                if (!resource) continue;

                ref var handleComponent = ref _taskAspect.Handle.Get(entity);
                ref var positionComponent = ref _taskAspect.Position.Get(entity);
                ref var parentComponent = ref _taskAspect.Parent.Get(entity);
                ref var rotationComponent = ref _taskAspect.Rotation.Get(entity);
                ref var targetComponent = ref _taskAspect.Target.Get(entity);
                ref var scaleComponent = ref _taskAspect.Scale.Get(entity);
                ref var parentEntityComponent = ref _taskAspect.ParentEntity.Get(entity);

                var parent = parentComponent.Value;
                var targetPosition = positionComponent.Value;
                var rotation = rotationComponent.Value;
                var instance = resource.Spawn(targetPosition, rotation, parent);
                lifeTime.AddCleanUpAction(() => instance.DespawnAsset());

                if (!instance) continue;

                //non gameobject case
                if (instance is not GameObject && instance is not Component) continue;

                var targetGameObject = instance is Component component
                    ? component.gameObject
                    : instance as GameObject;

                var spawnEntity = (ProtoEntity)(-1);
                if (targetComponent.Value.Unpack(_world, out var target))
                    spawnEntity = target;

                spawnEntity = (int)spawnEntity < 0 ? _world.NewEntity() : spawnEntity;

                ref var sourceLinkComponent = ref _resourceAspect.SourceLink.Add(spawnEntity);
                sourceLinkComponent.Source = handleComponent.Source;
                sourceLinkComponent.SpawnedEntity = _world.PackEntity(spawnEntity);

                if (parentEntityComponent.Value.Unpack(_world, out var parentEntity))
                {
                    ref var spawnParentEntityComponent = ref _resourceAspect.Parent.Add(spawnEntity);
                    spawnParentEntityComponent.Value = parentEntityComponent.Value;
                }

                if ((int)target > 0) _resourceAspect.Target.Add(spawnEntity);

                ref var spawnSpawnedComponent = ref _resourceAspect.SpawnedResource.Add(spawnEntity);
                ref var spawnObjectComponent = ref _resourceAspect.Object.Add(spawnEntity);
                ref var spawnResourceComponent = ref _resourceAspect.Resource.GetOrAddComponent(spawnEntity);

                spawnResourceComponent.Value = handleComponent.Resource;
                spawnSpawnedComponent.Source = handleComponent.Source;
                spawnObjectComponent.Value = instance;

                if (!targetGameObject) continue;

                //targetGameObject.transform.localScale = scaleComponent.Value;
                ref var spawnGameObjectComponent = ref _resourceAspect.GameObject.Add(spawnEntity);
                spawnGameObjectComponent.Value = targetGameObject;
            }
        }
    }
}