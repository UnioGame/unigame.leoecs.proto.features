namespace Game.Ecs.Scenes.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class SceneMapData
    {
        public Dictionary<int, SceneInfo> map;
        public Dictionary<string, SceneInfo> nameMap;
        public List<SceneInfo> collection;
        
        public SceneMapData(List<SceneInfo> scenes)
        {
            collection = scenes;
            map = scenes.ToDictionary(x => x.Value);
            nameMap = scenes.ToDictionary(x => x.Name);
        }
        
        public SceneInfo GetScene(int value)
        {
            return map.GetValueOrDefault(value);
        }
        
        public SceneInfo GetScene(string value)
        {
            return nameMap.GetValueOrDefault(value);
        }
    }
}