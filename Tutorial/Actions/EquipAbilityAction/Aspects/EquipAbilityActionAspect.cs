namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.EquipAbilityAction.Aspects
{
	using System;
	using AbilityInventory.Components;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class EquipAbilityActionAspect : EcsAspect
	{
		public ProtoPool<EquipAbilityActionComponent> EquipAbilityAction;
		public ProtoPool<EquipAbilityIdSelfRequest> EquipAbilityIdRequest;
		public ProtoPool<CompletedEquipAbilityActionComponent> CompletedEquipAbilityAction;
		public ProtoPool<ChampionComponent> Champion;
	}
}