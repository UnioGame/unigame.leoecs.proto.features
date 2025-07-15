namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
	using System;
	using System.Collections.Generic;
	using Ability.Aspects;
	using Aspects;
	using Components;
	using UniGame.Proto.Ownership;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Equip ability to slot
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilityEquipSystem : IProtoRunSystem
	{
		private AbilityInventoryAspect _inventoryAspect;
		private AbilityAspect _abilityAspect;
		private AbilityOwnerAspect _abilityOwnerAspect;
		private OwnershipAspect _ownershipAspect;
		
		private ProtoWorld _world;
		private List<ProtoEntity> _removedEntities = new List<ProtoEntity>();

		private ProtoIt _filter = It
			.Chain<AbilityInventoryCompleteComponent>()
			.Inc<EquipAbilitySelfRequest>()
			.Inc<AbilityBuildingComponent>()
			.Inc<OwnerLinkComponent>()
			.End();

		public void Run()
		{
			_removedEntities.Clear();
			
			foreach (var abilityEntity in _filter)
			{
				ref var requestComponent = ref _inventoryAspect.Equip.Get(abilityEntity);
				ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityEntity);

				var isUserInput  = requestComponent.IsUserInput;
				ref var slotId = ref requestComponent.AbilitySlot;
				ref var isDefault = ref requestComponent.IsDefault;
				ref var abilityId = ref requestComponent.AbilityId;
				var packedAbility =  _world.PackEntity(abilityEntity);

				if (!ownerLinkComponent.Value.Unpack(_world, out var ownerAbilityEntity))
					continue;
				
				ref var abilityMapComponent = ref _abilityOwnerAspect.AbilityMap.Get(ownerAbilityEntity);
				ref var slots = ref abilityMapComponent.AbilitySlots;
				if (slotId >= 0)
				{
					slots.TryGetValue(slotId,out var abilitySlotEntity);
					if (abilitySlotEntity.Unpack(_world, out var oldAbilityEntity))
						_removedEntities.Add(oldAbilityEntity);
					slots[slotId] = packedAbility;
					abilityMapComponent.Abilities.Remove(abilitySlotEntity);
				}

				abilityMapComponent.Abilities.Add(packedAbility);
				
				_abilityAspect.Active.Add(abilityEntity);

				if (isDefault)
				{
					_abilityAspect.Default.GetOrAddComponent(abilityEntity);
					_abilityAspect.ChangeInHandAbility(ownerAbilityEntity,abilityEntity);
				}

				var eventEntity = _world.NewEntity();
				ref var abilityEquipChangedEvent = ref _inventoryAspect.EquipChanged.Add(eventEntity);
				abilityEquipChangedEvent.AbilityId = abilityId;
				abilityEquipChangedEvent.AbilitySlot = slotId;
				abilityEquipChangedEvent.Owner = ownerLinkComponent.Value;
				abilityEquipChangedEvent.AbilityEntity = packedAbility;
				
				_inventoryAspect.Building.Del(abilityEntity);
				_inventoryAspect.Equip.Del(abilityEntity);
			}
			
			foreach (var removedEntity in _removedEntities)
				_world.DelEntity(removedEntity);
		}
	}
}