namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using GameLayers.Category;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [Serializable]
#if ODIN_INSPECTOR
    [HorizontalGroup(nameof(CategoryPriority), LabelWidth = 40)]
#endif
    public struct CategoryPriority
    {
        public CategoryId Category;
        public float Value;
    }
}