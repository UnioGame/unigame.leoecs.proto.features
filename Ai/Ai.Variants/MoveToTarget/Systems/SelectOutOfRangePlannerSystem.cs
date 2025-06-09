namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using Components;
    using Data;
    using Game.Ecs.Core.Death.Components;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using LeoEcs.Bootstrap;
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
    public class SelectOutOfRangePlannerSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private UnityAspect _unityAspect;
        private MoveToTargetAspect _moveToTargetAspect;
        
        private ProtoItExc _filter = It
            .Chain<MoveByRangeComponent>()
            .Inc<MoveToTargetPlannerComponent>()
            .Inc<TransformPositionComponent>()
            .Inc<MoveToGoalComponent>()
            .Inc<MoveOutOfRangeComponent>()
            .Exc<DisabledComponent>()
            .End();
        
        public void Run()
        {

            foreach (var entity in _filter)
            {
                ref var component = ref _moveToTargetAspect.ToGoal.Get(entity);
                ref var rangeComponent = ref _moveToTargetAspect.ByRange.Get(entity);
                ref var transformComponent = ref _unityAspect.Position.Get(entity);
                
                var center = rangeComponent.Center;
                
                var value = new MoveToGoalData
                {
                    Complete = false,
                    Position = center,
                    Priority = rangeComponent.Priority,
                    Target = _world.PackEntity(entity),
                    Effects = rangeComponent.Effects
                };
            
                component.Goals.Add(value);
                
                var minDistance = rangeComponent.MinDistance * rangeComponent.MinDistance;
                var distance = math.distancesq(transformComponent.Position, center);

                if (distance < minDistance) _moveToTargetAspect.OutOfRange.Del(entity);
            }
        }
    }
}