namespace Game.Modules.SequenceActions
{
    using System;
    using Abstract;
    using UnityEngine;

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
        public SequenceAction action = new();
        
        public string ActionName => action.ActionName;
    }

    [Serializable]
    public struct SequenceActionData
    {
        public float progress;
        
        [SerializeReference]
        public ISequenceAction action;
        
        public string ActionName => action == null ? "None" : action.ActionName;
    }
}