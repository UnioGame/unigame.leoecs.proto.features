namespace Game.Ecs.Scenes.Data
{
    using System;
    using Sirenix.OdinInspector;
    using UniGame.MultiScene.Runtime;

    [Serializable]
    public class SceneInfo
    {
        [OnValueChanged(nameof(UpdateInfo))]
        public string name = string.Empty;
        public int id;
        public bool useMultiScene = true;
        
        [ShowIf(nameof(useMultiScene))]
        public MultiSceneAsset sceneData;
        
        public string Name => name;
        public int Value => name.GetHashCode();

        public void UpdateInfo()
        {
            id = Name.GetHashCode();
        }
    }
}