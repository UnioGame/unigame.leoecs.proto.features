/*namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Area.Systems
{
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public sealed class DestroyAreaSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreaInstanceComponent>()
                .Exc<AreaLocalPositionComponent>()
                .End();
        }
        
        public void Run()
        {
            var areaInstancePool = _world.GetPool<AreaInstanceComponent>();

            foreach (var entity in _filter)
            {
                ref var areaInstance = ref areaInstancePool.Get(entity);
                
                if (areaInstance.Instance != null)
                    Object.Destroy(areaInstance.Instance);
                
                areaInstancePool.Del(entity);
            }
        }
    }
}*/