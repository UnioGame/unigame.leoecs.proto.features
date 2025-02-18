namespace Game.Ecs.Scenes.Components.Events
{
    using System;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Event that is triggered when a scene is unloaded.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SceneUnloadEvent
    {
        public Scene Scene;
    }
}