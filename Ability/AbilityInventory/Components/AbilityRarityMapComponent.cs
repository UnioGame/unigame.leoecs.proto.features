namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
	using System;
	using System.Collections.Generic;
	using Game.Code.Services.Ability.Data;

	/// <summary>
	///
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct AbilityRarityMapComponent
	{
		public AbilityRaritySlot DisableSlot;
		public List<AbilityRaritySlot> Slots;
	}
}