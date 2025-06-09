namespace Game.Code.Services.Ability.Data
{
	using System.Collections.Generic;
	using UnityEngine;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[CreateAssetMenu(menuName = "Game/Configurations/Ability/Ability Rarity Map", fileName = "Ability Rarity Map")]
	public class AbilityRarityData : ScriptableObject
	{
		public AbilityRaritySlot disableSlot = new AbilityRaritySlot();
        
#if ODIN_INSPECTOR
		[InlineProperty] 
#endif
		public List<AbilityRaritySlot> slots = new List<AbilityRaritySlot>();
	}
}