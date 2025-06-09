namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class EffectsRootData
    {
#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = "name")]
#endif
        public EffectRootKey[] roots = Array.Empty<EffectRootKey>();
    }
}