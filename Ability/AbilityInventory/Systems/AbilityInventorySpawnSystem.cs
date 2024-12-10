namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
	using System;
	using Ability.Common.Components;
	using Aspects;
	using Components;
	using Game.Code.Services.AbilityLoadout.Abstract;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilityInventorySpawnSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private AbilityInventoryAspect _inventoryAspect;
		private IAbilityCatalogService _service;
		
		private ProtoItExc _filter= It
			.Chain<AbilityInventoryProfileComponent>()
			.Inc<AbilityMapComponent>()
			.Exc<AbilityInventorySpawnDoneComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				foreach (var slotData in _service.AbilitySlotData)
				{
					var requestEntity = _world.NewEntity();
					ref var request = ref _inventoryAspect.EquipById.GetOrAddComponent(requestEntity);
					
					request.AbilityId = slotData.ability;
					request.AbilitySlot = slotData.slotType;
					request.Owner = _world.PackEntity(entity);
					request.IsDefault = slotData.slotType == 0;
				}
				
				_world.AddComponent<AbilityInventorySpawnDoneComponent>(entity);
			}
			
		}
	}
}