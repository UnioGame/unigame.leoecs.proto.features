namespace Game.Ecs.ButtonAction
{
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniModules.UniCore.Runtime.Utils;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
#endif
    
    /// <summary>
    /// Button action feature used in gameplay.
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Game Actions Feature",fileName = "Games Action Feature")]
    public class GamesActionFeature : BaseLeoEcsFeature
    {
        [SerializeReference]
        public List<IGameActionsSubFeature> features = new();
        
        [InlineEditor]
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "@FeatureName")]
        private List<GameActionsSubFeatureAsset> subFeatures = new();
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var tasks = features
                .Where(x => x != null)
                .Select(x => x.InitializeAsync(ecsSystems))
                .Concat(subFeatures
                    .Where(x => x!=null)
                    .Select(x => x.InitializeAsync(ecsSystems)));

            await UniTask.WhenAll(tasks);
        }

        [Button]
        public void FillFeatures()
        {
#if UNITY_EDITOR

            features.RemoveAll(x => x == null);
            subFeatures.RemoveAll(x => x == null);

            var featuresTypes = TypeCache.GetTypesDerivedFrom(typeof(IGameActionsSubFeature));
            var assetFeatures = AssetEditorTools.GetAssets<GameActionsSubFeatureAsset>();

            foreach (var subFeature in assetFeatures)
            {
                if (subFeatures.Contains(subFeature))
                    continue;
                subFeatures.Add(subFeature);
            }

            var thisPath = AssetDatabase.GetAssetPath(this)
                .Replace($"{name}.asset", string.Empty);
            
            foreach (var featureType in featuresTypes)
            {
                if(featureType.IsInterface || featureType.IsAbstract)
                    continue;
                if (features.Any(x => x.GetType() == featureType))
                    continue;
                
                var isScriptableObject = typeof(ScriptableObject).IsAssignableFrom(featureType);
                if(!featureType.HasDefaultConstructor() && !isScriptableObject)
                    continue;
                
                if (typeof(ScriptableObject).IsAssignableFrom(featureType))
                {
                    if(string.IsNullOrEmpty(thisPath)) continue;
                    var featureAsset = CreateInstance(featureType) as GameActionsSubFeatureAsset;
                    featureAsset.name = featureType.Name;
                    featureAsset.EditorInitialize(thisPath);
                    featureAsset.SaveAsset(thisPath);
                    subFeatures.Add(featureAsset);
                    continue;
                }
                
                var feature = featureType.CreateWithDefaultConstructor() as IGameActionsSubFeature;
                features.Add(feature);
            }
            
            this.MarkDirty();
#endif
        }
    }
}