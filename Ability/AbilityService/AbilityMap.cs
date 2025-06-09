namespace Game.Code.Configuration.Runtime.Ability
{
    using System.Collections.Generic;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(fileName = "Ability Map", menuName = "Game/Configurations/Ability/Ability Map")]
    public class AbilityMap : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineEditor()]
#endif
        [SerializeField]
        private List<AbilityConfiguration> _abilityConfigurations;

        public IReadOnlyList<AbilityConfiguration> Abilities => _abilityConfigurations;
    }
}