namespace Game.Ecs.Scenes.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Updates the active scenes in the game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateActiveScenesSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ScenesAspect _sceneAspect;
        
        private ProtoIt _sceneFilter = It
            .Chain<ActiveSceneComponent>()
            .Inc<HashComponent>()
            .Inc<NameComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _sceneFilter)
            {
                ref var activeComponent = ref _sceneAspect.ActiveScene.Get(entity);
                ref var nameComponent = ref _sceneAspect.Name.Get(entity);
                ref var hashComponent = ref _sceneAspect.Hash.Get(entity);

                var activeScene = SceneManager.GetActiveScene();
                var hash = hashComponent.Value;

                var activeHash = activeScene.path.GetHashCode();
                if (activeHash == hash) continue;
                
                activeComponent.Value = activeScene;
                hashComponent.Value = activeHash;
                nameComponent.Value = activeScene.name;
                
                _sceneAspect.ActiveSceneChanged.Add(entity);
            }
        }
    }
}