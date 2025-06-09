namespace Game.Ecs.ButtonAction
{
    using System.Collections.Generic;
    using System.Linq;
    using Components.Events;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    using GameActions;
    using GameActions.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.Runtime.Utils;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
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
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        public GameInputActionsFeature inputActions = new();
        
        [SerializeReference]
        public List<IGameActionsSubFeature> features = new();
        
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = "@FeatureName")]
        [InlineEditor]
#endif
        [SerializeField]
        private List<GameActionsSubFeatureAsset> subFeatures = new();
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.DelHere<GameActionEvent>();
            
            await inputActions.InitializeAsync(ecsSystems);
            
            var tasks = features
                .Where(x => x != null)
                .Select(x => x.InitializeAsync(ecsSystems))
                .Concat(subFeatures
                    .Where(x => x!=null)
                    .Select(x => x.InitializeAsync(ecsSystems)));

            ecsSystems.AddSystem(new HandleGameActionRequestSystem());
            
            //delete processed requests to activate game actions
            ecsSystems.DelHere<GameActionRequest>();
            ecsSystems.DelHere<GameActionSelfRequest>();
            
            await UniTask.WhenAll(tasks);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
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
            
            inputActions.EditorInitialize(thisPath);
            
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