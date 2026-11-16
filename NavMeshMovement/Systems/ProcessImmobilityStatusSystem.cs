namespace UniGame.Ecs.Proto.Movement.Systems
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Снимает обездвиживание, когда его больше никто не держит (счётчик источников дошёл до нуля), и не
    /// даёт обездвиженному агенту снова тронуться.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessImmobilityStatusSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _navigationAspect;

        private ProtoIt _filter = It.Chain<ImmobilityComponent>().End();

        private ProtoIt _stopFilter = It.Chain<ImmobilityComponent>()
            .Inc<NavMeshAgentComponent>()
            .Inc<MovementStopSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var block = ref _navigationAspect
                    .Immobility.Get(entity);
                
                if (block.BlockSourceCounter <= 0)
                    _navigationAspect.Immobility.Del(entity);
            }
            
            foreach (var entity in _stopFilter)
            {
                if(_navigationAspect.NavMeshAgentStop.Has(entity))
                    continue;

                _navigationAspect.NavMeshAgentStop.Add(entity);
            }
        }
    }
}