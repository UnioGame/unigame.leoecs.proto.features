/*namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using Component;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.Area.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessRadiusAreaAbilitySystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private LifeTimeAspect _lifeTimeAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AreableAbilityComponent>()
                .Inc<OwnerLinkComponent>()
                .End();
        }
        
        public void Run()
        {
            var radiusViewDataPool = _world.GetPool<RadiusViewDataComponent>();
            var avatarPool = _world.GetPool<EntityAvatarComponent>();
            var radiusViewPool = _world.GetPool<RadiusViewComponent>();
            
            foreach (var entity in _filter)
            {
                ref var ownerLinkComponent = ref _lifeTimeAspect.OwnerLink.Get(entity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                {
                    continue;
                }

                if (!radiusViewDataPool.Has(ownerEntity) || !avatarPool.Has(ownerEntity))
                    continue;
                
                ref var radiusViewData = ref radiusViewDataPool.Get(ownerEntity);
                ref var avatar = ref avatarPool.Get(ownerEntity);
                
                ref var radiusView = ref radiusViewPool.GetOrAddComponent(entity);
                radiusView.RadiusView = radiusViewData.ValidRadiusView;
                radiusView.Root = avatar.Feet;
            }
        }
    }
}*/