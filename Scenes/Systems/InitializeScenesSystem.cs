namespace Game.Ecs.Scenes.Systems
{
    using System;
    using Aspects;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Initializes the scenes in the game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class InitializeScenesSystem : IProtoInitSystem
    {
        private ProtoWorld _world;
        private ScenesAspect _sceneAspect;

        public void Init(IProtoSystems systems)
        {
            var entity = _world.NewEntity();
            
            ref var activeScene = ref _sceneAspect.ActiveScene.Add(entity);
            ref var nameComponent = ref _sceneAspect.Name.Add(entity);
            ref var hashComponent = ref _sceneAspect.Hash.Add(entity);
            var scene = SceneManager.GetActiveScene();
            
            var hash = scene.path.GetHashCode();
            activeScene.Value = scene;
            hashComponent.Value = hash;
            nameComponent.Value = scene.name;
        }
    }
}