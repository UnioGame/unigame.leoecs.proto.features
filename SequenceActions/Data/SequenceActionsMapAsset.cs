namespace Game.Modules.leoecs.proto.features.SequenceActions.Data
{
    using System;
    using System.Collections.Generic;
    using Modules.SequenceActions;
    using UniModules.Editor;
    using UniModules.UniGame.AddressableExtensions.Editor;
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

        [Button]
        public void CollectSequences()
        {
#if UNITY_EDITOR
            
            actions.Clear();
            var assets = AssetEditorTools.GetAssets<SequenceActionAsset>();
            foreach (var actionAsset in assets)
            {
                if (!actionAsset.IsAddressableAsset())
                {
                    actionAsset.MakeAddressable();
                    actionAsset.MarkDirty();
                }
                actions.Add(new SequenceActionItem()
                {
                    ActionName = actionAsset.ActionName,
                    ActionAsset = new AssetReferenceT<SequenceActionAsset>(actionAsset.GetGUID())
                });
            }

            this.MarkDirty();
#endif
        }
    }

    [Serializable]
    public class SequenceActionItem
    {
        public string ActionName;
        
        public AssetReferenceT<SequenceActionAsset> ActionAsset;
        
        public int Id => ActionName.GetHashCode();
    }
}