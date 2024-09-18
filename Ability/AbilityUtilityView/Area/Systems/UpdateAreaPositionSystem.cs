/*namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Area.Systems
{
    using System;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class UpdateAreaPositionSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private LifeTimeAspect _lifeTimeAspect;
        
        private ProtoPool<AreaInstanceComponent> _areaInstancePool;
        private ProtoPool<AreaLocalPositionComponent> _areaPositionPool;
        private ProtoPool<TransformPositionComponent> _transformPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AreaInstanceComponent>()
                .Inc<AreaLocalPositionComponent>()
                .Inc<OwnerLinkComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var ownerLinkComponent = ref _lifeTimeAspect.OwnerLink.Get(entity);
                ref var ownerPackedEntity = ref ownerLinkComponent.Value;
                if(!ownerPackedEntity.Unpack(_world, out var ownerEntity) || 
                   !_transformPool.Has(ownerEntity))
                    continue;

                ref var ownerTransform = ref _transformPool.Get(ownerEntity);
                ref var areaInstance = ref _areaInstancePool.Get(entity);
                ref var areaPosition = ref _areaPositionPool.Get(entity);

                var ownerPosition = ownerTransform.Position;
                var position = areaPosition.Value;

                areaInstance.Instance.transform.position = ownerPosition + position;
            }
        }
    }
}*/