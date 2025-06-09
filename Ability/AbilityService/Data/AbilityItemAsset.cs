namespace Game.Code.Services.AbilityLoadout.Data
{
	using System.Linq;

	using UniGame.Core.Runtime;
	using UnityEngine;
	
#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
#if UNITY_EDITOR
	using UniModules.Editor;
#endif
	
	[CreateAssetMenu(menuName = "Game/Ability/Ability Item",fileName = nameof(AbilityItemAsset))]
	public class AbilityItemAsset : ScriptableObject, IUnique
	{
		public const string AbilityItemGroupName = "Ability Info";

		#region inspector
		
#if ODIN_INSPECTOR
		[BoxGroup(AbilityItemGroupName, order:-1)]
		[InlineProperty]
		[HideLabel]
#endif
		public AbilityItemData data = new AbilityItemData();

		#endregion

		public int Id
		{
			get => data.Id;
			set => data.id = value;
		}

#if UNITY_EDITOR
		
#if ODIN_INSPECTOR
		[Button]
#endif
		public void GetNewId()
		{
			var assets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			var maxId = assets.Max(x => x.Id);
			data.id = maxId + 1;
			this.MarkDirty();
		}

#if ODIN_INSPECTOR
		[OnInspectorInit]
#endif
		public void EditorInitialize()
		{
			if (data.id == 0)
			{
				GetNewId();
			}
		}

#endif
	}
}