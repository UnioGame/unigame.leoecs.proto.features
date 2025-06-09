namespace UniGame.Ecs.Proto.Presets.Demo
{
    using System.Collections.Generic;
    using Assets;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    public class DemoSceneSettingsChanger : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [FoldoutGroup("Runtime")] 
        [HideLabel]
#endif
        public RenderingSettingsPreset runtime;

#if ODIN_INSPECTOR
        [FoldoutGroup("Source")] 
        [HideLabel]
#endif
        public RenderingSettingsPreset source;
        
        public List<RenderingSettingsPreset> settings = new List<RenderingSettingsPreset>();

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Apply()
        {
            runtime.ApplyToRendering();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Reset()
        {
            runtime.ApplySettings(source);
        }
        
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button]
#endif

        private void RemoveHighlight()
        {

        }
#endif
    }
}