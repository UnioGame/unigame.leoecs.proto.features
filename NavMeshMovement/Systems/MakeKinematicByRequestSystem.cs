namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

    /// <summary>
    /// Включает или выключает кинематику у Rigidbody по запросу SetKinematicSelfRequest.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MakeKinematicByRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAspect _navigationAspect;
        
        private ProtoIt _rigidbodyFilter = It
            .Chain<RigidbodyComponent>()
            .Inc<SetKinematicSelfRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _rigidbodyFilter)
            {
                ref var request = ref _navigationAspect.SetKinematicStatus.Get(entity);
                ref var navMeshAgentComponent = ref _navigationAspect.Rigidbody.Get(entity);
                navMeshAgentComponent.Value.isKinematic = request.Value;
            }
        }
    }
}