namespace Game.Ecs.Scenes.Services
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using UniGame.GameFlow.Runtime;
    using UniGame.MultiScene.Runtime;
    
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;

    [Serializable]
    public class SceneService : GameService, ISceneService
    {
        private SceneMapData _sceneData;

        public SceneService(SceneMapData sceneData)
        {
            _sceneData = sceneData;
        }
        
        public async UniTask LoadSceneAsync(int scene,
            LoadSceneMode mode, bool reload, bool activate)
        {
            var sceneInfo = _sceneData.GetScene(scene);
            await LoadSceneAsync(sceneInfo.Name,mode,reload, activate);
        }
        
        public async UniTask LoadSceneAsync(
            string sceneName, 
            LoadSceneMode mode,
            bool reload, bool activate)
        {
            var sceneInfo = _sceneData.GetScene(sceneName);

            if (sceneInfo is { useMultiScene: true } && 
                sceneInfo.sceneData!=null)
            {
                var multiScene = sceneInfo.sceneData;
                if (reload == false && multiScene.IsLoaded())
                    return;

                var multiSceneHandle = await multiScene.OpenScenesAsync(mode, reload);
                return;
            }
            
            var scene = SceneManager.GetSceneByName(sceneName);

            if (!reload && scene.isLoaded)
                return;

            var addressableResult = await LoadAddressableSceneAsync(sceneName, mode, activate);
            if (addressableResult) return;

            await LoadSceneAsync(sceneName, mode, activate);
        }

        public async UniTask<bool> LoadAddressableSceneAsync(string sceneName, LoadSceneMode mode, bool activate)
        {
            var handle = Addressables.LoadSceneAsync(sceneName, mode, activate);
            var sceneInstance = await handle.Task;
            return sceneInstance.Scene.isLoaded;
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activate)
        {
            await SceneManager.LoadSceneAsync(sceneName, mode);
            if (!activate) return;
            
            var sceneValue = SceneManager.GetSceneByName(sceneName);
            if (string.IsNullOrEmpty(sceneValue.name)) return;
            SceneManager.SetActiveScene(sceneValue);
        }
        
    }
}