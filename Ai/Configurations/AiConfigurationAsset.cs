namespace UniGame.Ecs.Proto.AI.Configurations
{

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(menuName = "Game/Configurations/AI/Ai Configuration",fileName = nameof(AiConfigurationAsset))]
    public class AiConfigurationAsset : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public AiConfiguration configuration = new AiConfiguration();
    }
}
