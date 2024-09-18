/*namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Component;
    using Game.Ecs.Core;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.Selection.Components;
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
    public sealed class ProcessRadiusForTargetAbilitySystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private LifeTimeAspect _lifeTimeAspect;
        
        private ProtoPool<TransformPositionComponent> _positionPool;
        private ProtoPool<RadiusComponent> _radiusPool;
        private ProtoPool<SelectedTargetsComponent> _targetsPool;
        private ProtoPool<RadiusViewDataComponent> _radiusViewDataPool;
        private ProtoPool<EntityAvatarComponent> _avatarPool;
        private ProtoPool<RadiusViewComponent> _radiusViewPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<AbilityInHandComponent>()
                //.Inc<TargetableAbilityComponent>()
                .Inc<SelectedTargetsComponent>()
                .Inc<OwnerLinkComponent>()
                .Inc<RadiusComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var ownerLinkComponent = ref _lifeTimeAspect.OwnerLink.Get(entity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                {
                    continue;
                }

                if(!_radiusViewDataPool.Has(ownerEntity) || !_avatarPool.Has(ownerEntity))
                    continue;

                ref var radiusViewData = ref _radiusViewDataPool.Get(ownerEntity);
                ref var avatar = ref _avatarPool.Get(ownerEntity);
                ref var targets = ref _targetsPool.Get(entity);
                
                ref var ownerTransform = ref _positionPool.Get(ownerEntity);
                var ownerPosition = ownerTransform.Position;
                
                ref var radius = ref _radiusPool.Get(entity);
                
                var hasAnyValidTargets = false;
                var radiusValue = radius.Value * radius.Value;
                
                for (int i = 0; i < targets.Count; i++)
                {
                    ref var packedEntity = ref targets.Entities[i];
                    if(!packedEntity.Unpack(_world, out var targetEntity) || 
                       !_positionPool.Has(targetEntity) || !_avatarPool.Has(targetEntity))
                        continue;

                    ref var targetTransform = ref _positionPool.Get(targetEntity);
                    var targetPosition = targetTransform.Position;
                    ref var targetAvatar = ref _avatarPool.Get(targetEntity);

                    var value = EntityHelper.IsSqrClosest(
                        ref ownerPosition,
                        ref targetPosition, 
                        ref targetAvatar.Bounds, radiusValue);
                    
                    if (!value.isClosest) continue;

                    hasAnyValidTargets = true;
                    break;
                }
                
                var selectedRadiusView = hasAnyValidTargets 
                    ? radiusViewData.ValidRadiusView 
                    : radiusViewData.InvalidRadiusView;

                ref var radiusView = ref _radiusViewPool.GetOrAddComponent(entity);
                radiusView.RadiusView = selectedRadiusView;
                radiusView.Root = avatar.Feet;
            }
        }
    }
}*/