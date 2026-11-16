namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
	using System;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Служебные данные способности: id, слот, скрыта ли, заблокирована ли.
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public struct AbilityMetaComponent
	{
		public int AbilityId;
		public int SlotType;
		public bool Hide;
		public bool IsBlocked;
	}
}