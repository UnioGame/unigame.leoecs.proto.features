namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using Converters;
    using UniModules.Editor;
#endif
    
    [CreateAssetMenu(menuName = "Game/Effects/Effects Root Configuration", fileName = "Effects Root Configuration")]
    public class EffectsRootConfiguration : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif

        public EffectsRootData data;

#if ODIN_INSPECTOR
        [FolderPath] 
#endif
        public string[] bakeLocations = Array.Empty<string>();

#if ODIN_INSPECTOR
        [ButtonGroup(nameof(BakeAll))]
#endif
        public void BakeAll()
        {
#if UNITY_EDITOR
            var targets = AssetEditorTools.GetAssets<GameObject>(bakeLocations);
            foreach (var target in targets)
            {
                Bake(target);
            }
#endif
        }
        
#if UNITY_EDITOR
        public void Bake(GameObject target)
        {
            var converter = target.GetComponent<EffectRootMonoConverter>();
            converter = converter == null ? target.AddComponent<EffectRootMonoConverter>() : converter;
                
            converter.BakeActive();
            converter.MarkDirty();
            target.MarkDirty();
        }
#endif
    }
}