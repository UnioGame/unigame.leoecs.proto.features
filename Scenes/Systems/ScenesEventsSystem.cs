namespace Game.Ecs.Scenes.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components.Events;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// System for handling scene events in the game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ScenesEventsSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        private ProtoWorld _world;
        private ScenesAspect _sceneAspect;
        
        private List<SceneLoadedEvent> _loadedScenes = new();
        private List<Scene> _unloadedScenes = new();
        
        public void Init(IProtoSystems systems)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void Destroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
        
        public void Run()
        {
            var activeScene = SceneManager.GetActiveScene();

            foreach (var sceneValue in _loadedScenes)
            {
                var sceneEvent = _world.NewEntity();
                ref var sceneLoaded = ref _sceneAspect.SceneLoaded.Add(sceneEvent);

                var scene = sceneValue.Scene;
                var isActive = scene.handle == activeScene.handle;
                    
                sceneLoaded.Scene = scene;
                sceneLoaded.IsActive = isActive;
                sceneLoaded.Mode = sceneValue.Mode;
            }

            foreach (var unloadedScene in _unloadedScenes)
            {
                var sceneEvent = _world.NewEntity();
                ref var unloadEvent = ref _sceneAspect.SceneUnload.Add(sceneEvent);
                unloadEvent.Scene = unloadedScene;
            }
                
            _loadedScenes.Clear();
            _unloadedScenes.Clear();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var sceneLoaded = new SceneLoadedEvent
            {
                Scene = scene,
                Mode = mode
            };
            
            _loadedScenes.Add(sceneLoaded);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            _unloadedScenes.Add(scene);
        }
    }
}