namespace UniGame.Ecs.Proto.Selection
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Game.Code.GameLayers.Category;
    using Game.Code.GameLayers.Layer;
    using Game.Ecs.TargetSelection;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using TargetSelection;
    using TargetSelection.Aspects;
    using TargetSelection.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [ECSDI]
    [Serializable]
    public class TargetSelectionSystem : IProtoDestroySystem
    {
        private EntityFloat[] _selectionBuffer = new EntityFloat[TargetSelectionData.MaxTargets];
        private ProtoPackedEntity[] _packedBuffer = new ProtoPackedEntity[TargetSelectionData.MaxTargets];
        private ProtoEntity[] _entitiesBuffer = new ProtoEntity[TargetSelectionData.MaxTargets];

        private TargetSelectionAspect _targetSelectionAspect;
        private TargetAspect _targetAspect;
        private ProtoWorld _world;

        private List<int> _queryResult = new(TargetSelectionData.MaxAgents);

        private ProtoIt _kdDataFilter = It
            .Chain<KDTreeDataComponent>()
            .Inc<KDTreeComponent>()
            .Inc<KDTreeQueryComponent>()
            .End();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SelectEntitiesInArea(
            ProtoEntity[] resultContainer,
            float radius,
            ref float3 sourcePosition,
            ref LayerId layerId,
            ref CategoryId categoryId)
        {
            var result = resultContainer;
            var counter = 0;

            var amount = SelectEntitiesInArea(_entitiesBuffer, _packedBuffer, radius, ref sourcePosition);
            amount = math.min(TargetSelectionData.MaxTargets, amount);

            for (var i = 0; i < amount; i++)
            {
                var entity = _entitiesBuffer[i];
                if (!PassesMask(entity, ref layerId, ref categoryId)) continue;
                // the caller's buffer may be older and smaller than MaxTargets
                if (counter >= result.Length) break;

                result[counter] = entity;
                counter++;
            }

            return counter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SelectEntitiesInArea(
            ProtoPackedEntity[] result,
            float radius,
            ref float3 sourcePosition,
            ref LayerId layerId,
            ref CategoryId categoryId)
        {
            var counter = 0;

            var amount = SelectEntitiesInArea(_entitiesBuffer, _packedBuffer, radius, ref sourcePosition);
            amount = math.min(TargetSelectionData.MaxTargets, amount);

            for (var i = 0; i < amount; i++)
            {
                var entity = _entitiesBuffer[i];
                ref var packedEntity = ref _packedBuffer[i];

                if (!PassesMask(entity, ref layerId, ref categoryId)) continue;
                // the caller's buffer may be older and smaller than MaxTargets
                if (counter >= result.Length) break;

                result[counter] = packedEntity;
                counter++;
            }

            return counter;
        }

        public int SelectEntitiesInArea(
            ProtoEntity[] result,
            ProtoPackedEntity[] packedResult,
            float radius,
            ref float3 sourcePosition)
        {
            foreach (var kdEntity in _kdDataFilter)
            {
                _queryResult.Clear();

                ref var dataComponent = ref _targetAspect.Data.Get(kdEntity);
                ref var kdTreeComponent = ref _targetAspect.Tree.Get(kdEntity);
                ref var queryComponent = ref _targetAspect.Query.Get(kdEntity);

                var tree = kdTreeComponent.Value;
                var query = queryComponent.Value;

                query.Radius(tree, sourcePosition, radius, _queryResult);

                var amount = _queryResult.Count;
                amount = math.min(TargetSelectionData.MaxTargets, amount);

                var counter = 0;
                for (var i = 0; i < amount; i++)
                {
                    var index = _queryResult[i];
                    ref var packed = ref dataComponent.PackedEntities[index];
                    if (!packed.Unpack(_world, out var entity))
                        continue;

                    packedResult[counter] = packed;
                    result[counter] = entity;
                    counter++;
                }

                return counter;
            }

            return 0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IsPassedByMask(
            ref ProtoPackedEntity packedEntity,
            ref LayerId layerId,
            ref CategoryId categoryId)
        {
            if (!packedEntity.Unpack(_world, out var entity))
                return -1;

            return PassesMask(entity, ref layerId, ref categoryId) ? (int)entity : -1;
        }


        // Every entity with Health is in the kd-tree; only those with both a layer and a category can be targeted.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool PassesMask(ProtoEntity entity, ref LayerId layerId, ref CategoryId categoryId)
        {
            if (!_targetSelectionAspect.Layer.Has(entity) || !_targetSelectionAspect.Category.Has(entity))
                return false;

            var layer = _targetSelectionAspect.Layer.Get(entity).Value;
            var category = _targetSelectionAspect.Category.Get(entity).Value;
            return (layerId & layer) == layer && (categoryId & category) == category;
        }

        public void Destroy()
        {
        }
    }
}