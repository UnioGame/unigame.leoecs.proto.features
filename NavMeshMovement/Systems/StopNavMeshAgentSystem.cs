namespace UniGame.Ecs.Proto.Movement.Systems.NavMesh
{
    using System;
    using Aspect;
    using Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Останавливает агентов, которые отключены или уже уничтожаются, чтобы они не продолжали идти.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class StopNavMeshAgentSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private NavMeshAgentAspect _navigationAspect;

        private ProtoIt _deadFilter = It
            .Chain<NavMeshAgentComponent>()
            .Inc<DestroyComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _navigationAspect.DisabledAgentFilter)
            {
                if(!_navigationAspect.NavMeshAgentStop.Has(entity))
                    _navigationAspect.NavMeshAgentStop.Add(entity);
            }
            
            foreach (var entity in _deadFilter)
            {
                if(!_navigationAspect.NavMeshAgentStop.Has(entity))
                    _navigationAspect.NavMeshAgentStop.Add(entity);
            }
        }
    }
}