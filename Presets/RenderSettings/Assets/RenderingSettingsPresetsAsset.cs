namespace UniGame.Ecs.Proto.Presets.Assets
{

    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [CreateAssetMenu(menuName = "Game/Presets/LightingPreset", fileName = "LightingPreset")]
    public class RenderingSettingsPresetsAsset : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif 
        [FormerlySerializedAs("settings")]
        public RenderingSettingsPreset settingsPreset = new RenderingSettingsPreset();
    }
}