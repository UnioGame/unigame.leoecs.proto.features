namespace UniGame.Ecs.Proto.ViewControl.Systems
{
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class ProcessShowViewRequestSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShowViewRequest>().End();
        }
        
        public void Run()
        {
            var requestPool = _world.GetPool<ShowViewRequest>();
            var viewDataPool = _world.GetPool<ViewDataComponent>();
            var viewInstancePool = _world.GetPool<ViewInstanceComponent>();

            foreach (var entity in _filter)
            {
                ref var request = ref requestPool.Get(entity);
                if(!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                ref var viewData = ref viewDataPool.GetOrAddComponent(destinationEntity);
                if (viewData.Views.TryGetValue(request.View, out var viewPackedEntity))
                {
                    if (viewPackedEntity.Unpack(_world, out var viewEntity))
                    {
                        ref var viewInstance = ref viewInstancePool.Get(viewEntity);
                        viewInstance.Count++;
                    }

                    continue;
                }

                var instance = Object.Instantiate(request.View, request.Root);
                instance.transform.localPosition = Vector3.zero;
                var localScale = request.Root.localScale;
                instance.transform.localScale = new Vector3(request.Size.x / localScale.x, request.Size.y / localScale.y, request.Size.z / localScale.z);

                var newViewEntity = _world.NewEntity();
                ref var newViewInstance = ref viewInstancePool.Add(newViewEntity);
                newViewInstance.Count++;
                newViewInstance.ViewInstance = instance;
                
                viewData.Views.Add(request.View, _world.PackEntity(newViewEntity));

                if (!request.Destination.Unpack(_world, out var unpackedDestination))
                {
                    GameLog.LogError("Cannot unpack destination entity");
                    continue;
                }
                
                unpackedDestination.AddChild(newViewEntity, _world);
            }
        }
    }
}