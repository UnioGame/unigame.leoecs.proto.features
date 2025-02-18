namespace Game.Ecs.Scenes.Services
{
    using Cysharp.Threading.Tasks;
    using UniGame.GameFlow.Runtime.Interfaces;
    using UnityEngine.SceneManagement;

    public interface ISceneService : IGameService
    {
        UniTask LoadSceneAsync(
            int sceneName, 
            LoadSceneMode mode,
            bool reload, bool activate);
        
        UniTask LoadSceneAsync(
            string sceneName, 
            LoadSceneMode mode,
            bool reload, bool activate);

        UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activate);
        UniTask<bool> LoadAddressableSceneAsync(string sceneName, LoadSceneMode mode, bool activate);
    }
}