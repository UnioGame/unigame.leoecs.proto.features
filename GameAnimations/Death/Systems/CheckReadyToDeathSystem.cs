namespace UniGame.Ecs.Proto.Gameplay.Death.Systems
{
    using System;
    using Game.Ecs.Characteristics.Health.Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Core.Death.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
    [Serializable]
    public class CheckReadyToDeathSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _readyFilter;
        private ProtoPool<KillRequest> _killPool;


        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();

            _readyFilter = _world
                .Filter<PrepareToDeathComponent>()
                .Exc<AwaitDeathCompleteComponent>()
                .End();
            
            _killPool = _world.GetPool<KillRequest>();
        }

        public void Run()
        {
            foreach (var readyEntity in _readyFilter)
            {
                /*ref var readyComponent = ref _readyPool.Get(readyEntity);
                ref var killRequest = ref _killPool.GetOrAddComponent(readyEntity);
                killRequest.Source = readyComponent.Source;
                
                _readyPool.Del(readyEntity);*/
            }
        }
    }
    
    
}