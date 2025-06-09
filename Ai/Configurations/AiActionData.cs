namespace UniGame.Ecs.Proto.AI.Configurations
{
    using System;
    using Abstract;
    using Core.Runtime;
    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AiActionData : ISearchFilterable
    {
        private const int LabelWidth = 100;
        
#if ODIN_INSPECTOR
        [LabelWidth(LabelWidth)]
#endif
        [FormerlySerializedAs("_name")]
        [SerializeField]
        public string name = string.Empty;
        
#if ODIN_INSPECTOR
        [LabelWidth(LabelWidth)]
        [InlineProperty]
#endif
        [FormerlySerializedAs("_planner")]
        [SerializeReference]
        public IAiPlannerSystem planner;

#if ODIN_INSPECTOR
        [LabelWidth(LabelWidth)]
        [InlineProperty]
#endif
        [FormerlySerializedAs("_action")]
        [SerializeReference]
        public IAiActionSystem action;

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (name.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

            var typeName = planner?.GetType().Name;
            if (typeName != null && typeName.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
            
            typeName = action?.GetType().Name;
            if (typeName != null && typeName.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

            return false;
        }
    }
}