namespace Game.Code.Timeline.Addressables
{
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AddressableGameObjectData
    {
#if ODIN_INSPECTOR
        [DrawWithUnity]
#endif
        
        public AssetReferenceGameObject asset;
        
        public bool makeInstance;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(makeInstance))]
#endif
        public bool useParent = false;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(makeInstance))]
#endif
        public Vector3 position;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(makeInstance))]
#endif
        public Vector3 rotation;
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(makeInstance))]
#endif
        public Vector3 scale = new Vector3(1f,1f,1f);
        
#if ODIN_INSPECTOR
        [ShowIf(nameof(makeInstance))]
#endif
        public bool stayWorldPosition = false;
    }
}