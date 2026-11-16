namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;

	/// <summary>
	/// Эффект с задержкой: начнёт действовать только через Delay секунд.
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct DelayedEffectComponent
	{
		public float Delay;
		public float LastApplyingTime;
		public EffectConfiguration Configuration;
	}
}