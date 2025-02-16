namespace UniGame.Ecs.Proto.GameResources.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Code.DataBase.Runtime.Abstract;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessSpawnRequestSystem : IProtoRunSystem
    {
        private IGameDatabase _gameDatabase;
        private ProtoWorld _world;
        private GameResourceAspect _gameResourceAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoIt _filter = It
            .Chain<GameResourceSpawnRequest>()
            .End();

        public ProcessSpawnRequestSystem(IGameDatabase gameDatabase)
        {
            _gameDatabase = gameDatabase;
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var spawnRequest = ref _gameResourceAspect.SpawnRequest.Get(entity);
                
                var entityLifeTime = _ownershipAspect.LifeTime.Add(entity);
                var resourceLifeTime = spawnRequest.LifeTime ?? entityLifeTime.LifeTime;
                
                var loadResourceTask = _gameDatabase
                    .LoadAsync<UnityEngine.Object>(spawnRequest.ResourceId, resourceLifeTime);
                
                ref var resourceSpawnComponent = ref _gameResourceAspect.SpawnResource.Add(entity);
                resourceSpawnComponent.LocationData = spawnRequest.LocationData;
                resourceSpawnComponent.ResourceLifeTime = resourceLifeTime;
                resourceSpawnComponent.Parent = spawnRequest.Parent;

                ref var loadTaskComponent = ref _gameResourceAspect.LoadTask.Add(entity);
                loadTaskComponent.Value = loadResourceTask;
            }
        }
    }
}