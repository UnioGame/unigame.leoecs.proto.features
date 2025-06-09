namespace Game.Code.Services.AbilityLoadout
{
	using Ability.Data;
	using Code.AbilityLoadout;
	using Cysharp.Threading.Tasks;
	using Data;
	using UniGame.AddressableTools.Runtime;
	using UniGame.Core.Runtime;
	using UniGame.Context.Runtime;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	[CreateAssetMenu(menuName = "Game/Services/AbilityLoadoutService Source", fileName = "AbilityLoadoutService Source")]
	public class AbilityLoadoutServiceSource : DataSourceAsset<IAbilityCatalogService>
	{
		public AssetReferenceT<AbilityDataBase> abilityDatabase;
		public AssetReferenceT<AbilitySlotsData> abilitySlotMap;
		public AssetReferenceT<AbilityRarityData> abilityRarityData;
		
		protected override async UniTask<IAbilityCatalogService> CreateInternalAsync(IContext context)
		{
			var data = new AbilityLoadoutData();
			
			var abilityDataBase = await abilityDatabase
				.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			var slotMap = await abilitySlotMap
				.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			var rarityMap = await abilityRarityData
				.LoadAssetInstanceTaskAsync(context.LifeTime, true);
			
			data.abilityDataBase = abilityDataBase;
			data.abilitySlotMap = slotMap;
			data.abilityRarityData = rarityMap;
			
			var profileData = new AbilityProfileData();
			context.Publish(profileData);
            
			var service = new AbilityLoadoutService(profileData,data);
			return service;
		}
		
	}
}