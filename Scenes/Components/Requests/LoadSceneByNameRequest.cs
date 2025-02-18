namespace Game.Ecs.Scenes.Components.Requests
{
    using System;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Represents a request to load a scene.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct LoadSceneByNameRequest
    {
        public string scene;
        public LoadSceneMode LoadMode;
        public bool ActivateOnLoad;
        public bool Reload;
    }
}