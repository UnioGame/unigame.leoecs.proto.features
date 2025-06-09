namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(fileName = "Ability Id Data", menuName = "Game/Configurations/Arena/Abilities/Ability Type Id Data", order = 0)]
    public class AbilityCategoryIdData : ScriptableObject
    {
        public AbilityCategory defaultCategory = new AbilityCategory();

#if ODIN_INSPECTOR
        [InlineProperty] 
#endif
        public AbilityCategory[] categoryIds = Array.Empty<AbilityCategory>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AbilityCategory GetInfo(int id)
        {
            if (id < 0 || id >= categoryIds.Length)
                return defaultCategory;

            return categoryIds[id];
        }
    }
}