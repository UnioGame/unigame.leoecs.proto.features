namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using DataBase.Runtime.Abstract;

	using UniGame.Runtime.Utils;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
	public class AbilityRecord : IGameResourceRecord
	{
		public static AbilityRecord Empty = new AbilityRecord()
		{
			id = -1,
			name = "EMPTY",
			ability = new AssetReferenceAbility()
		};
		
		public string name;
		public int id;
		public int slotType;
		
#if ODIN_INSPECTOR
		[TitleGroup("ability data")]
		[InlineProperty]
		[HideLabel]
#endif
		public AbilityData data = new AbilityData();
		
#if ODIN_INSPECTOR
		[TitleGroup("ability behaviour")]
		[InlineProperty]
		[HideLabel]
#endif
		public AssetReferenceAbility ability;
		
		public string Name => name;
		
		public string ResourcePath => name;

		public string Id => name;
		
		public bool CheckRecord(string filter)
		{
			return filter.Equals(name,StringComparison.OrdinalIgnoreCase);
		}

		public string Label => $"{name} | ({id})";

		public bool IsMatch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString)) return true;
			if (id.ToStringFromCache().IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
			if (Name != null && Name.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

#if UNITY_EDITOR
			if (ability.EditorValue != null &&
			    ability.EditorValue.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
#endif
			return false;
		}
	}
}