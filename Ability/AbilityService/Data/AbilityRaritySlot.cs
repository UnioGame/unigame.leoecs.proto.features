namespace Game.Code.Services.Ability.Data
{
	using System;
	using System.Collections.Generic;
	using AbilityLoadout.Data;
	using UniGame.Core.Runtime;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
	public class AbilityRaritySlot
	{
#if ODIN_INSPECTOR
		[ValueDropdown(nameof(GetRarity))]
#endif
		public int id;

		public AssetReferenceT<Sprite> background;
		
		private IEnumerable<ValueDropdownItem<int>> GetRarity()
		{
			foreach (var abilitySlot in AbilitySlotId.GetAbilitySlots())
			{
				yield return new ValueDropdownItem<int>()
				{
					Text = abilitySlot.Text,
					Value = abilitySlot.Value
				};
			}
		}
	}
}