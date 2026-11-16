namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
	using System;
	using Game.Code.Configuration.Runtime.Ability;
	using Leopotam.EcsProto.QoL;

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
	public struct EquipAbilityIdSelfRequest
	{
		public int AbilityId;
		public int AbilitySlot;
		public bool IsDefault;
		public ProtoPackedEntity Owner;
	}
}