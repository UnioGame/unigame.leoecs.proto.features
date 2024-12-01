namespace UniGame.Ecs.Proto.Ability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using AbilityUtilityView.Components;
    using AbilityUtilityView.Radius.AggressiveRadius.Components;
    using AbilityUtilityView.Radius.Component;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Layer;
    using Game.Ecs.Core;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using GameLayers.Category.Components;
    using GameLayers.Layer.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAggressiveAbilityRadiusSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private ProtoPool<TransformPositionComponent> _positionPool;
        private ProtoPool<AggressiveRadiusViewDataComponent> _viewDataPool;
        private ProtoPool<CategoryIdComponent> _gameCategoryPool;
        private ProtoPool<LayerIdComponent> _layerPool;
        private ProtoPool<AggressiveRadiusViewStateComponent> _statePool;
        private ProtoPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;

        private ProtoPool<RadiusComponent> _radiusPool;

        //private ProtoPool<AbilityTargetsComponent> _chosenPool;
        private ProtoPool<EntityAvatarComponent> _avatarPool;
        private ProtoPool<ShowRadiusRequest> _showRadiusPool;
        private ProtoPool<HideRadiusRequest> _hideRadiusPool;

        private List<ProtoEntity> _result = new();
        private List<ProtoEntity> _selectedDestinations = new();
        private List<ProtoPackedEntity> _destinationsEntities = new();

        private ProtoItExc _filter = It
            .Chain<AggressiveRadiusViewDataComponent>()
            .Inc<AggressiveRadiusViewStateComponent>()
            .Inc<TransformPositionComponent>()
            .Inc<VisibleUtilityViewComponent>()
            .Exc<DestroyComponent>()
            .End();

        private ProtoIt _categoryFilter = It
            .Chain<LayerIdComponent>()
            .Inc<LayerIdComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var viewData = ref _viewDataPool.Get(entity);

                _result.Clear();
                _selectedDestinations.Clear();

                FindAllPossibleDestinations(
                    _result,
                    _gameCategoryPool,
                    _layerPool, viewData.CategoryId,
                    viewData.LayerMask);

                ref var positionComponent = ref _positionPool.Get(entity);
                var ourPosition = positionComponent.Position;

                foreach (ProtoEntity destination in _result)
                {
                    if (!_positionPool.Has(destination)) continue;

                    if (!_abilityInHandLinkPool.Has(destination)) continue;

                    if (!_avatarPool.Has(destination)) continue;

                    ref var abilityLink = ref _abilityInHandLinkPool.Get(destination);
                    if (!abilityLink.AbilityEntity.Unpack(_world, out var abilityEntity))
                        continue;

                    ref var abilityRadius = ref _radiusPool.Get(abilityEntity);
                    ref var destinationPosition = ref _positionPool.Get(destination);
                    ref var avatar = ref _avatarPool.Get(destination);

                    var distance = EntityHelper.GetSqrDistance(
                        ref ourPosition,
                        ref destinationPosition.Position,
                        ref avatar.Bounds);

                    var noTargetDistance = abilityRadius.Value * abilityRadius.Value * 1.5f;
                    var closeTargetDistance = abilityRadius.Value * abilityRadius.Value * 1.2f;
                    var hasTargetDistance = abilityRadius.Value * abilityRadius.Value;

                    if (distance > noTargetDistance) continue;

                    GameObject radiusView = null;

                    if (distance < noTargetDistance)
                    {
                        radiusView = viewData.NoTargetRadiusView;
                    }

                    var chosenTargetCount = 0;
                    var isChosenUs = false;

                    var showRequestEntity = _world.NewEntity();
                    ref var showRequest = ref _showRadiusPool.Add(showRequestEntity);

                    showRequest.Source = _world.PackEntity(entity);
                    showRequest.Destination = _world.PackEntity(destination);

                    showRequest.Radius = radiusView;
                    showRequest.Root = avatar.Feet;

                    var size = abilityRadius.Value * 2.0f;
                    showRequest.Size = new Vector3(size, size, size);

                    _selectedDestinations.Add(destination);
                }

                ref var state = ref _statePool.Get(entity);

                _destinationsEntities.Clear();

                _world.PackAll(_destinationsEntities, _selectedDestinations);

                state.SetEntities(_destinationsEntities);

                foreach (var packedEntity in state.PreviousEntities)
                {
                    if (state.Entities.Contains(packedEntity)) continue;

                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _hideRadiusPool.Add(hideRequestEntity);

                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasTargetWithId(ProtoWorld world, ProtoPackedEntity[] packedEntities, ProtoEntity entity)
        {
            foreach (var packedEntity in packedEntities)
            {
                if (!packedEntity.Unpack(world, out var targetEntity))
                    continue;

                if (targetEntity.Equals(entity))
                    return true;
            }

            return false;
        }

        private void FindAllPossibleDestinations(
            List<ProtoEntity> result,
            ProtoPool<CategoryIdComponent> gameCategoryPool,
            ProtoPool<LayerIdComponent> layerMaskPool,
            CategoryId categoryId,
            LayerId layerMask)
        {
            foreach (var entity in _categoryFilter)
            {
                ref var gameCategoryComponent = ref gameCategoryPool.Get(entity);
                if ((gameCategoryComponent.Value & categoryId) != categoryId)
                    continue;

                ref var gameLayerComponent = ref layerMaskPool.Get(entity);
                if (!layerMask.HasFlag(gameLayerComponent.Value))
                    continue;

                result.Add(entity);
            }
        }
    }
}