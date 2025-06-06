namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DisableNavMeshAgentSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoWorld _world;
        
        private ProtoIt _filter;
        private ProtoItExc _excFilter;
        
        private NavMeshAspect _navigationAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<NavMeshAgentComponent>()
                .Inc<DisabledComponent>()
                .End();
            
            _excFilter = _world
                .Filter<NavMeshAgentComponent>()
                .Exc<DisabledComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                if(navMeshAgentComponent.Value.enabled)
                    navMeshAgentComponent.Value.enabled = false;
            }
            
            foreach (var entity in _excFilter)
            {
                ref var navMeshAgentComponent = ref _navigationAspect.Agent.Get(entity);
                if(!navMeshAgentComponent.Value.enabled)
                    navMeshAgentComponent.Value.enabled = true;
            }
        }
    }
}