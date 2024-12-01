namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using Unity.Mathematics;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public class SelectByRangePlannerSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private UnityAspect _unityAspect;
        private MoveToTargetAspect _moveToTargetAspect;
        
        private ProtoItExc _filter = It
            .Chain<MoveByRangeComponent>()
            .Inc<MoveToTargetPlannerComponent>()
            .Inc<TransformPositionComponent>()
            .Exc<MoveOutOfRangeComponent>()
            .Exc<DisabledComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var rangeComponent = ref _moveToTargetAspect.ByRange.Get(entity);
                ref var transformComponent = ref _unityAspect.Position.Get(entity);

                ref var position = ref transformComponent.Position;
                var center = rangeComponent.Center;
                var distance = math.distancesq(position, center);
                var sqrRadius = rangeComponent.Radius * rangeComponent.Radius;

                if (distance <= sqrRadius) continue;

                _moveToTargetAspect.OutOfRange.Add(entity);
            }
        }
    }
}