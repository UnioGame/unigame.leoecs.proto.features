namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Converters
{
    using System;
    using Components;
    using Game.Code.GameLayers.Category;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public class MoveToPointActionTargetConverter : LeoEcsConverter
    {
        [SerializeField]
        private int _priority = -1;

        [SerializeField]
        private Transform _point;

        [SerializeField]
        [CategoryIdMask]
        public CategoryId _category;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            var point = _point == null
                ? target.transform
                : _point;

            ref var component = ref world.AddComponent<MoveToPoiComponent>(entity);
            component.Position = point.position;
            component.Priority = _priority;
            component.CategoryId = _category;
        }
    }
}