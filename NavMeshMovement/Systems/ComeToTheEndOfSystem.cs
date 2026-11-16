namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ComeToTheEndOfSystem : IProtoRunSystem,IProtoInitSystem
    {
        private const float ArriveDistance = 0.1f;

        private ProtoWorld _world;
        private NavMeshAspect _navigationAspect;
        private ProtoIt _filter = It.Chain<ComePointComponent>()
            .Inc<TransformPositionComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var transform = ref _navigationAspect.Position.Get(entity);
                ref var comePoint = ref _navigationAspect.ComePoint.Get(entity);

                var currentPosition = transform.Position;
                var distance = math.distance(currentPosition, comePoint.Value);

                // a NavMesh agent stops near the point (stopping distance), never exactly on it
                if (distance < ArriveDistance)
                {
                    _navigationAspect.ComePoint.Del(entity);
                    continue;
                }

                ref var movementPoint = ref _navigationAspect
                    .MovementTargetPoint
                    .GetOrAddComponent(entity);
                
                movementPoint.Value = comePoint.Value;
            }
        }

    }
}