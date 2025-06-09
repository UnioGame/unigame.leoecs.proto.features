namespace Game.Ecs.Scenes.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;

    /// <summary>
    /// unity scene aspect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ScenesAspect : EcsAspect
    {
        // Filters
        public ProtoIt SceneLoadedFilter = It
            .Chain<SceneLoadedEvent>()
            .End();
        
        // Active scene data
        public ProtoPool<ActiveSceneComponent> ActiveScene;
        public ProtoPool<NameComponent> Name;
        public ProtoPool<HashComponent> Hash;
        
        // Represents a request to load a scene.
        public ProtoPool<LoadSceneRequest> LoadScene;
        public ProtoPool<LoadSceneByNameRequest> LoadSceneByName;
        
        // Event that is triggered when a scene is loaded.
        public ProtoPool<SceneLoadedEvent> SceneLoaded;
        // Event that is triggered when a scene is unloaded.
        public ProtoPool<SceneUnloadEvent> SceneUnload;
        // Notify about active scene changed
        public ProtoPool<ActiveSceneChangedSelfEvent> ActiveSceneChanged;
    }
}