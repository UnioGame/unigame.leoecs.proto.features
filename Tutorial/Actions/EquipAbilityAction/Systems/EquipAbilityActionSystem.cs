namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.EquipAbilityAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Equip ability to champion.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class EquipAbilityActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private EquipAbilityActionAspect _aspect;


		private ProtoIt _championFilter = It
			.Chain<ChampionComponent>()
			.End();
		
		private ProtoItExc _abilityActionFilter= It
			.Chain<EquipAbilityActionComponent>()
			.Exc<CompletedEquipAbilityActionComponent>()
			.End();

		public void Run()
		{
			if (_championFilter.IsEmpty()) return;
			
			var championEntity = _championFilter.First().Entity;
			
			foreach (var equipAbilityActionEntity in _abilityActionFilter)
			{
				ref var equipAbilityActionComponent = ref _aspect.EquipAbilityAction.Get(equipAbilityActionEntity);
				
                foreach (var abilityCell in equipAbilityActionComponent.AbilityCells)
				{
					var abilityId = abilityCell.AbilityId;
					var slot = abilityCell.SlotId;
				
					var requestEntity = _world.NewEntity();
					ref var request = ref _aspect.EquipAbilityIdRequest.Add(requestEntity);
					request.AbilityId = abilityId;
					request.AbilitySlot = slot;
					request.IsDefault = slot == 0;
					request.Owner = championEntity.PackEntity(_world);
				}
                _aspect.CompletedEquipAbilityAction.Add(equipAbilityActionEntity);
			}
		}
	}
}