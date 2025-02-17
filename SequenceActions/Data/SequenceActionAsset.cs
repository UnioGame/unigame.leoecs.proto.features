namespace Game.Modules.SequenceActions
{
    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/SequenceActions/SequenceActionAsset",
        fileName = "SequenceActionAsset")]
    public class SequenceActionAsset : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        public SequenceActions actions = new();
        
        public string ActionName => name;
    }
}