namespace Game.Code.Timeline.Addressables
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AddressableAssetLoaderAnimationBehaviour : AddressableAnimationBehaviour
    {
#if ODIN_INSPECTOR
        [DrawWithUnity]
#endif
        public List<AssetReference> assets = new List<AssetReference>();

        
        public override void Load(GameObject source,float inputWeight, float progress)
        {
            var lifeTime = source.GetAssetLifeTime();
            
            foreach (var asset in assets)
                asset.LoadAssetTaskAsync<Object>(lifeTime).Forget();
        }
    }
}