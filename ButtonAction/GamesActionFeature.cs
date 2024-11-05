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
    [CreateAssetMenu(menuName = "Proto Features/Game Actions/Button Action Feature")]
    public class GamesActionFeature : BaseLeoEcsFeature
    {
        [SerializeReference]
        public List<IGameActionsSubFeature> features = new();
        
        [InlineEditor]
        [SerializeField]
        private List<GameActionsSubFeature> subFeatures = new();
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var tasks = features
                .Where(x => x != null)
                .Select(x => x.InitializeActions(ecsSystems))
                .Concat(subFeatures
                    .Where(x => x!=null)
                    .Select(x => x.InitializeActions(ecsSystems)));

            await UniTask.WhenAll(tasks);
        }

        [Button]
        public void FillFeatures()
        {
#if UNITY_EDITOR

            features.RemoveAll(x => x == null);
            subFeatures.RemoveAll(x => x == null);

            var featuresTypes = TypeCache.GetTypesDerivedFrom(typeof(IGameActionsSubFeature));
            var assetFeatures = AssetEditorTools.GetAssets<GameActionsSubFeature>();

            foreach (var subFeature in assetFeatures)
            {
                if (subFeatures.Contains(subFeature))
                    continue;
                subFeatures.Add(subFeature);
            }
            
            foreach (var featureType in featuresTypes)
            {
                if(featureType.IsInterface || featureType.IsAbstract)
                    continue;
                if(!featureType.HasDefaultConstructor())
                    continue;
                if (features.Any(x => x.GetType() == featureType))
                    continue;
                if(typeof(Object).IsAssignableFrom(featureType))
                    continue;
                
                var feature = featureType.CreateWithDefaultConstructor() as IGameActionsSubFeature;
                features.Add(feature);
            }
            
            this.MarkDirty();
#endif
        }
    }
}