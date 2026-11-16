namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Включает или выключает NavMeshAgent по запросу SetNavigationStatusSelfRequest.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SetNavigationStatusByRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private NavMeshAspect _navigationAspect;

        private ProtoIt _filter = It
            .Chain<NavMeshAgentComponent>()
            .Inc<SetNavigationStatusSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _navigationAspect.SetNavigationStatus.Get(entity);
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                navMeshAgentComponent.Value.enabled = request.Value;
            }
        }
    }
}