namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using Configuration.Runtime.Ability;
	using Configuration.Runtime.Ability.Description;

	using UniGame.Core.Runtime;
	using UniGame.Runtime.Utils;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
	public class AbilityItemData : IUnique, ISearchFilterable
	{
		public static readonly AbilityItemData EmptyItem = new AbilityItemData();

#if ODIN_INSPECTOR
		[TitleGroup("settings")]
#endif
		public int id;
		
#if ODIN_INSPECTOR
		[TitleGroup("settings")]
		[InlineProperty]
		[HideLabel]
#endif
		public AbilityData data = new();

#if ODIN_INSPECTOR
		[TitleGroup("behaviour")]
#endif
		public AssetReferenceT<AbilityConfiguration> configurationReference;
		
#if ODIN_INSPECTOR
		[TitleGroup("visual")]
		[InlineProperty]
		[HideLabel]
#endif
		[SerializeField]
		public AbilityVisualDescription visualDescription;
		
		public int Id => id;
		
		public bool IsMatch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString)) return true;
			
			var slotName = AbilitySlotId.GetSlotName((AbilitySlotId)data.slotType);
			if(CheckString(slotName, searchString)) return true;
			if(CheckString(id.ToStringFromCache(), searchString)) return true;

			return false;
		}
		
		private bool CheckString(string target, string search)
		{
			if (string.IsNullOrEmpty(target)) return false;
			return target.Contains(search, StringComparison.OrdinalIgnoreCase);
		}
		
	}
}