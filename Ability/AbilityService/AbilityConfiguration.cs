namespace Game.Code.Configuration.Runtime.Ability
{
    using System.Collections.Generic;
    using Description;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [CreateAssetMenu(fileName = "Ability Configuration", menuName = "Game/Ability/Ability Configuration")]
    public sealed class AbilityConfiguration : ScriptableObject
    {
#if ODIN_INSPECTOR
        [PropertySpace(8)]
#endif
        [SerializeReference]
        public List<IAbilityBehaviour> abilityBehaviours = new List<IAbilityBehaviour>();
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Save()
        {
#if UNITY_EDITOR
            this.SaveAsset();
#endif
        }
    }
}