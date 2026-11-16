namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Components
{
	using System;

	/// <summary>
	/// Метка: эффект урона уже отработал. Сейчас не используется: урон наносится на каждое применение
	/// эффекта.
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct DamageEffectRequestCompleteComponent
	{
		
	}
}