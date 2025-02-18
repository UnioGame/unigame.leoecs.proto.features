namespace Game.Ecs.Scenes.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Services;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Loads scenes asynchronously based on LoadSceneByNameRequest from entities.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class LoadScenesSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ScenesAspect _sceneAspect;
        
        private ISceneService _sceneInfoData;

        private ProtoIt _sceneFilter = It
            .Chain<LoadSceneRequest>()
            .End();
        
        private ProtoIt _sceneNameFilter = It
            .Chain<LoadSceneByNameRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _sceneFilter)
            {
                ref var loadRequest = ref _sceneAspect.LoadScene.Get(entity);
                _sceneInfoData.LoadSceneAsync(loadRequest.scene, loadRequest.LoadMode,
                    loadRequest.Reload, loadRequest.ActivateOnLoad).Forget();
            }

            foreach (var entity in _sceneNameFilter)
            {
                ref var loadRequest = ref _sceneAspect.LoadSceneByName.Get(entity);
                _sceneInfoData.LoadSceneAsync(loadRequest.scene, loadRequest.LoadMode,
                    loadRequest.Reload, loadRequest.ActivateOnLoad).Forget();
            }
        }
        
        
    }

}