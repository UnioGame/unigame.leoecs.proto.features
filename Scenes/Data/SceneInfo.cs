namespace Game.Ecs.Scenes.Data
{
    using System;
    using UniGame.MultiScene.Runtime;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class SceneInfo
    {
#if ODIN_INSPECTOR
        [OnValueChanged(nameof(UpdateInfo))]
#endif
        public string name = string.Empty;
        public int id;
        public bool useMultiScene = true;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(useMultiScene))]
#endif
        public MultiSceneAsset sceneData;
        
        public string Name => name;
        public int Value => name.GetHashCode();

        public void UpdateInfo()
        {
            id = Name.GetHashCode();
        }
    }
}