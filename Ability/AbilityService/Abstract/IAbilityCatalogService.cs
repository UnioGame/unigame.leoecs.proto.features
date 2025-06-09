namespace Game.Code.AbilityLoadout
{
	using System.Collections.Generic;
	using Cysharp.Threading.Tasks;
	using Services.Ability.Data;
	using Services.AbilityLoadout.Data;
	using UniGame.GameFlow.Runtime;

	public interface IAbilityCatalogService : IGameService
	{
		int[] AllAbilities { get; }
		
		int[] InventoryAbilities { get; }
        
		IReadOnlyList<AbilitySlot> Slots { get; }

		IReadOnlyList<int> Inventory { get; }
        
		IReadOnlyList<AbilitySlotData> AbilitySlotData { get; }
		
		AbilityRarityData AbilityRarityData { get; }

		UniTask<AbilityItemData> GetAbilityDataAsync(int abilityId);

		UniTask<bool> EquipAsync(int abilityId, int slotType);

		UniTask<AbilityItemData> CreateAbilityAsync(AbilitySlotId slotType);
	}
}