namespace UniGame.Ecs.Proto.AI.Configurations
{
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(menuName = "Game/Configurations/AI/Ai Agent Configuration",fileName = nameof(AiConfigurationAsset))]
    public class AiAgentConfigurationAsset : ScriptableObject,ILeoEcsGizmosDrawer
    {
#if ODIN_INSPECTOR
        [InlineProperty] 
        [HideLabel] 
#endif
        [FormerlySerializedAs("_agentConfiguration")]
        [SerializeField]
        public AiAgentConfiguration agentConfiguration = new AiAgentConfiguration();

#if ODIN_INSPECTOR
        [InlineEditor()]
#endif
        [FormerlySerializedAs("_aiConfiguration")]
        [SerializeField]
        public AiConfigurationAsset aiConfiguration;
        
        public AiAgentConfiguration AiAgentConfiguration => agentConfiguration;

        public AiConfiguration AiConfiguration => aiConfiguration.configuration;

        public int ActionsCount => aiConfiguration
            .configuration
            .aiActions.Length;
        
        public void DrawGizmos(GameObject target)
        {
            foreach (var planner in agentConfiguration.Planners)
            {
                if(planner is not ILeoEcsGizmosDrawer drawer) continue;
                drawer.DrawGizmos(target);
            }
        }
    }
}