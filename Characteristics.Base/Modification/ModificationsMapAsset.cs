namespace UniGame.Ecs.Proto.Characteristics
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Modifications/Modifications Map",fileName = "Modifications Map")]
    public class ModificationsMapAsset : ScriptableObject,IModificationsMap
    {
#if ODIN_INSPECTOR
        [HideLabel]
        [InlineProperty]
#endif
        public ModificationsMap map = new ModificationsMap();

        public IEnumerable<string> Modifications => map.Modifications;

        public ModificationInfo GetModificationInfo(Type type) => map.GetModificationInfo(type);

        public ModificationHandler Create(string type,float value) => map.Create(type,value);
        public ModificationHandler Create(Type type, float value) => map.Create(type, value);
    }
}