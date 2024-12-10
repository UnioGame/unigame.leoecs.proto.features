namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Search ability in inventory
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class EquipAbilityByIdSystem : IProtoRunSystem
	{
		private AbilityInventoryAspect _inventoryAspect;
		
		private ProtoWorld _world;
		
		private ProtoIt _filterRequest = It
			.Chain<EquipAbilityIdSelfRequest>()
			.End();
		
		public void Run()
		{
			foreach (var abilityEntity in _filterRequest)
			{
				ref var requestComponent = ref _inventoryAspect.EquipById.Get(abilityEntity);
				ref var abilityEquipRequest = ref _inventoryAspect.Equip.Add(abilityEntity);
				abilityEquipRequest.AbilityId = requestComponent.AbilityId;
				abilityEquipRequest.AbilitySlot = requestComponent.AbilitySlot;
				abilityEquipRequest.Target = requestComponent.Owner;
				abilityEquipRequest.IsDefault = requestComponent.IsDefault;

				_inventoryAspect.EquipById.Del(abilityEntity);
			}
		}
	}
}