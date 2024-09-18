namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class DestroyAreaByOwnerSystem : IProtoRunSystem, IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        private ProtoPool<AreaInstanceComponent> _areaInstancePool;
        private ProtoPool<KillRequest> _killRequest;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<OwnerDestroyedEvent>()
                .Inc<AreaInstanceComponent>()
                .End();
            
            _areaInstancePool = _world.GetPool<AreaInstanceComponent>();
            _killRequest = _world.GetPool<KillRequest>();
        }
        
        public void Run()
        {
            
            foreach (var entity in _filter)
            {
                ref var areaInstance = ref _areaInstancePool.Get(entity);
                _areaInstancePool.Del(entity);
                
                if (areaInstance.Instance == null)
                    continue;
                
                Object.Destroy(areaInstance.Instance);
            }
        }
    }
}