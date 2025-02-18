namespace Game.Ecs.Scenes
{
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Data;
    using Systems;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Services;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Scenes/Game Scenes Feature",fileName = "Game Scenes Feature")]
    public class GameScenesFeature : BaseLeoEcsFeature
    {
        [SerializeField]
        private SceneMap sceneMap;
        
        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var sceneMapInstantiate = Instantiate(sceneMap);
            var context = ecsSystems.GetService<IContext>();
            
            var sceneMapData = new SceneMapData(sceneMapInstantiate.collection);
            var sceneService = new SceneService(sceneMapData);
            
            context.Publish<ISceneService>(sceneService);
            world.SetGlobal(sceneMapData);
            world.SetGlobal<ISceneService>(sceneService);

            // Delete used SceneLoadedEvent
            ecsSystems.DelHere<SceneLoadedEvent>();
            // Delete used SceneLoadedEvent
            ecsSystems.DelHere<SceneUnloadEvent>();
            
            // Initializes the scenes in the game.
            ecsSystems.Add(new InitializeScenesSystem());
            
            // Loads scenes asynchronously based on LoadSceneByNameRequest from entities.
            ecsSystems.Add(new LoadScenesSystem());
            
            // Delete used LoadSceneByNameRequest
            ecsSystems.DelHere<LoadSceneRequest>();
            ecsSystems.DelHere<LoadSceneByNameRequest>();
            
            // System for handling scene events in the game.
            ecsSystems.Add(new ScenesEventsSystem());

            // Delete used ActiveSceneChangedSelfEvent
            ecsSystems.DelHere<ActiveSceneChangedSelfEvent>();
            // Updates the active scenes in the game.
            ecsSystems.Add(new UpdateActiveScenesSystem());
            
            return UniTask.CompletedTask;
        }
    }
}