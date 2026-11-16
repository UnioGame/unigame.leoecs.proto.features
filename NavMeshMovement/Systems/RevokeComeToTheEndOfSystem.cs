namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Отменяет движение «до упора»: убирает ComePoint и текущую точку движения, останавливает агента.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RevokeComeToTheEndOfSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAspect _navigationAspect;

        private ProtoIt _filter = It
            .Chain<ComePointComponent>()
            .Inc<RevokeComeToEndOfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                _navigationAspect.ComePoint.Del(entity);
                
                if(_navigationAspect.MovementTargetPoint.Has(entity))
                    _navigationAspect.MovementTargetPoint.Del(entity);
                
                if (!_navigationAspect.NavMeshAgentStop.Has(entity))
                    _navigationAspect.NavMeshAgentStop.Add(entity);
            }
        }
    }
}