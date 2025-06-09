namespace UniGame.Ecs.Proto.AI.Configurations
{
    using System;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AiConfiguration
    {
#if ODIN_INSPECTOR
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [ListDrawerSettings(ListElementLabelName = "@name")]
#endif
        public AiActionData[] aiActions = Array.Empty<AiActionData>();
    }
}