namespace Game.Code.Services.AbilityLoadout.Data
{
	using System.Collections.Generic;
	using UnityEngine;
	
#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[CreateAssetMenu(menuName = "Game/Configurations/Ability/Ability Slots Map",fileName = nameof(AbilitySlotsData))]
	public class AbilitySlotsData : ScriptableObject
	{
#if ODIN_INSPECTOR
		[InlineProperty]
#endif
		public List<AbilitySlot> slots = new List<AbilitySlot>();
	}
}