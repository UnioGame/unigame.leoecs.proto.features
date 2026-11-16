namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Система отвечающая за остановку симуляции NavMesh при наличии запроса на остановку <see
    /// cref="UniGame.Ecs.Proto.Movement.Components.MovementStopSelfRequest"/>.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class NavMeshAgentStopSimulationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAspect _navigationAspect;

        private ProtoIt _filter = It
            .Chain<NavMeshAgentComponent>()
            .Inc<MovementStopSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var navMeshAgent = ref _navigationAspect.Agent.Get(entity);

                var agent = navMeshAgent.Value;
                
                if(!navMeshAgent.Value.enabled || !agent.isOnNavMesh)
                    continue;
                
                navMeshAgent.Value.isStopped = true;
            }
        }
    }
}