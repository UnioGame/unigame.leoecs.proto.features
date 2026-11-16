namespace UniGame.Ecs.Proto.Core.Death.Components
{
	using System;

	/// <summary>
	/// Метка «ненастоящая» смерть: сущность считается умершей по сценарию, а не от потери здоровья.
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct ArtificialDeathComponent
	{
		
	}
}