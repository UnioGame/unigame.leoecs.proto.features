namespace Game.Modules.leoecs.proto.features.SequenceActions.Data
{
    using System;
    using System.Collections.Generic;
    using Modules.SequenceActions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;  
#endif
    
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/SequenceActions/SequenceActionsMapAsset",
        fileName = "SequenceActionsMapAsset")]
    public class SequenceActionsMapAsset : ScriptableObject
    {
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = "@ActionName")]
#endif
        public List<SequenceActionItem> actions = new();
    }

    [Serializable]
    public class SequenceActionItem
    {
        public int Id;
        public string ActionName;
        public AssetReferenceT<SequenceActionAsset> ActionAsset;
    }
}