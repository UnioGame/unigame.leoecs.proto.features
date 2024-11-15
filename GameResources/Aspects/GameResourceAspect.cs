namespace UniGame.Ecs.Proto.GameResources.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;

    [Serializable]
    public class GameResourceAspect : EcsAspect
    {
        public ProtoPool<GameResourceSpawnComponent> Spawn;
        public ProtoPool<GameResourceLoadTaskComponent> LoadTask;
        
        //requests
        public ProtoPool<GameResourceSpawnRequest> SpawnRequest;
        public ProtoPool<ResourceInstanceSpawnRequest> InstanceSpawnRequest;
    }
}