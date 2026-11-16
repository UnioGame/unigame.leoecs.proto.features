namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
	using System;
	using Leopotam.EcsProto.QoL;

	/// <summary>
	/// Request to equip ability
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct EquipAbilitySelfRequest
	{
		public int AbilityId;
		public int AbilitySlot;
		public bool IsUserInput;
		public bool IsDefault;
		public bool IsBlocked;
		public bool Hide;
		public ProtoPackedEntity Target;
	}

}