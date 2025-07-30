namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Cysharp.Threading.Tasks;
	using DataBase.Runtime;
	using DataBase.Runtime.Abstract;

	using UniGame.Core.Runtime;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
#if UNITY_EDITOR
	using UniModules.Editor;
	using UniGame.AddressableTools.Editor;
#endif
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	/// <summary>
	/// Ability category
	/// </summary>
	[CreateAssetMenu(menuName = "Game/Ability/Ability Database",fileName = "AbilityDatabase")]
	public sealed class AbilityDataBase : GameDataCategory
	{
		public string id = "Ability";
		
#if ODIN_INSPECTOR
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		[PropertyOrder(2)]
		[ListDrawerSettings(ListElementLabelName = "@Label")]
#endif
		public List<AbilityRecord> abilities = new();
		private IGameResourceProvider _resourceProvider = new AddressableResourceProvider();
		private Dictionary<string, IGameResourceRecord> _map = new();
		private IGameResourceProvider _resourceProvider1;

		public override IGameResourceProvider ResourceProvider => _resourceProvider1;

		public override IReadOnlyList<IGameResourceRecord> Records => abilities;

		public override async UniTask<CategoryInitializeResult> InitializeAsync(ILifeTime lifeTime)
		{
			_map.Clear();
			foreach (var ability in abilities)
			{
				_map[ability.name] = ability;
			}

			return new CategoryInitializeResult()
			{
				category = this,
				complete = true,
				error = string.Empty,
				categoryName = id,
			};
		}

		public override IGameResourceRecord Find(string filter)
		{
			foreach (var ability in abilities)
			{
				if (ability.CheckRecord(filter))
					return ability;
			}

			return default;
		}

		public override IReadOnlyList<IGameResourceRecord> FindResources(string filter)
		{
			return abilities
				.Where(x => x.name.Contains(filter, StringComparison.OrdinalIgnoreCase))
				.Select(x => x as IGameResourceRecord)
				.ToList();
		}

		public override Dictionary<string, IGameResourceRecord> Map => _map;

		public AbilityRecord Find(int recordId)
		{
			foreach (var abilityRecord in abilities)
			{
				if (abilityRecord.id == recordId)
					return abilityRecord;
			}
			return AbilityRecord.Empty;
		}
		
#if ODIN_INSPECTOR
		[Button(ButtonSizes.Large,Icon = SdfIconType.ArchiveFill)]
#endif
		public override IReadOnlyList<IGameResourceRecord> FillCategory()
		{
			var abilityItems = new List<AbilityRecord>();
			
#if UNITY_EDITOR
			var abilityItemAssets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			abilityItemAssets.Sort((x,y) => Comparer<int>.Default.Compare(x.Id,y.Id));
			
			foreach (var item in abilityItemAssets)
			{
				if(item == null || !item.IsInAnyAddressableAssetGroup()) continue;
                
				var record = new AbilityRecord();

				var itemData = item.data;
				record.name = item.name;
				record.id = itemData.id;
				record.data = itemData.data;
				
				record.ability = new AssetReferenceAbility()
				{
					reference = new AssetReferenceT<AbilityItemAsset>(item.GetGUID())
				};
                
				abilityItems.Add(record);
			}

			abilities = abilityItems;
			
			this.MarkDirty();
#endif

			return abilityItems;
		}
		
#if UNITY_EDITOR

#if ODIN_INSPECTOR
		[Button]
#endif
		private void ValidateMeta()
		{
			var abilityItemAssets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			foreach (var abilityItemAsset in abilityItemAssets)
			{
				if(abilityItemAsset.data.visualDescription == null)
					Debug.LogError($"Ability {abilityItemAsset.name} has no configuration reference",abilityItemAsset);
			}
		}
		
#endif
	}
}