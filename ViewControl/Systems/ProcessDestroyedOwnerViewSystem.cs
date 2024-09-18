namespace UniGame.Ecs.Proto.ViewControl.Systems
{
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessDestroyedOwnerViewSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private ProtoPool<ViewInstanceComponent> _viewInstancePool;
        private ProtoPool<KillRequest> _killPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<ViewInstanceComponent>()
                .Inc<OwnerDestroyedEvent>()
                .End();
            
            _viewInstancePool = _world.GetPool<ViewInstanceComponent>();
            _killPool = _world.GetPool<KillRequest>();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _killPool.GetOrAddComponent(entity);
                _world.DelEntity(entity);
            }
        }
    }
}