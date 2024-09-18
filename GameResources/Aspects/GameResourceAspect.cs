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
        public ProtoPool<GameSpawnedResourceComponent> SpawnedResource;
        public ProtoPool<GameResourceIdComponent> Resource;
        public ProtoPool<UnityObjectComponent> Object;
        public ProtoPool<GameSpawnCompleteComponent> Complete;
        public ProtoPool<GameResourceSourceLinkComponent> SourceLink;

        //optional
        public ProtoPool<GameObjectComponent> GameObject;
        public ProtoPool<ParentEntityComponent> Parent;
        public ProtoPool<GameResourceSelfTargetComponent> Target;
        
        //requests
        public ProtoPool<GameResourceSpawnRequest> Spawn;
        
        //events
        public ProtoPool<GameResourceSpawnCompleteEvent> SpawnComplete;
    }
}