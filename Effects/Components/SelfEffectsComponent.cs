namespace UniGame.Ecs.Proto.Effects.Components
{
	using System;
	using System.Collections.Generic;
	using Game.Code.Configuration.Runtime.Effects.Abstract;
	using LeoEcs.Proto;
	using Leopotam.EcsProto;

	/// <summary>
	/// Эффекты, которые способность накладывает на своего владельца.
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct SelfEffectsComponent : IProtoAutoReset<SelfEffectsComponent>
	{
		public List<IEffectConfiguration> Effects;
        
		public void SetHandlers(IProtoPool<SelfEffectsComponent> pool) => pool.SetResetHandler(AutoReset);
		
		public static void AutoReset(ref SelfEffectsComponent c)
		{
			c.Effects ??= new List<IEffectConfiguration>();
			c.Effects.Clear();
		}
	}
}