namespace UniGame.Ecs.Proto.Characteristics
{
    using System;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class ModificationData : ISearchFilterable
    {
        public string id;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Modification")]
        [HideLabel]
        [InlineProperty]
#endif
        public ModificationInfo info = new ModificationInfo();
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Modification")]
        [InlineProperty]
        [HideLabel]
        [Sirenix.OdinInspector.Required]
#endif
        [SerializeReference]
        public IModificationFactory factory;

        public Type ModificationType => factory.TargetType;

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (id.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
            if (info!=null && info.description.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
            if (factory!=null && factory.GetType().Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }

    [Serializable]
    public class ModificationInfo
    {
        public static readonly ModificationInfo Empty = new ModificationInfo()
        {
            descriptionColor = Color.magenta,
            description = "EMPTY"
        };
        
        public string description;
        public Color descriptionColor = Color.black;
        public Color valueColor = Color.blue;
        public bool revertValue = false;

    }
}