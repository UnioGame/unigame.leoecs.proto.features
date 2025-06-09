namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "Ability Catalog", menuName = "Game/Ability/Ability Catalog", order = -1)]
    public class AbilityCatalog : ScriptableObject
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        public GameplayAbilityRecord[] abilities = Array.Empty<GameplayAbilityRecord>();
        
        public bool Find(int id, out AbilityCategoryId categoryId, out int level, out bool isPassive)
        {
            foreach (var abilityRecord in abilities)
            {
                foreach (var abilityRef in abilityRecord.abilityAssets)
                {
                    if (abilityRef.id != id) continue;
                    
                    categoryId = abilityRecord.categoryId;
                    level = abilityRef.level;
                    isPassive = abilityRecord.isPassive;
                    return true;
                }
            }

            categoryId = default;
            level = -1;
            isPassive = false;
            return false;
        }
        
        public IEnumerable<AbilityCategoryId> GetCategories()
        {
            return abilities.Select(x => x.categoryId);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void UpdateAbilitiesLevels()
        {
#if UNITY_EDITOR
            foreach (var abilityRecord in abilities)
            {
                var i = 1;
                foreach (var abilityRef in abilityRecord.abilityAssets)
                {
                    abilityRef.level = i++;
                }
            }
            EditorUtility.SetDirty(this);
#endif
        }
    }
}