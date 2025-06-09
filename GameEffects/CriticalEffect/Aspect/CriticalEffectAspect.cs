namespace UniGame.Ecs.Proto.GameEffects.CriticalEffect.Aspect
{
	using System;
	using Components;
	using Effects.Components;
	using Gameplay.CriticalAttackChance.Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;
	
	[Serializable]
	public class CriticalEffectAspect : EcsAspect
	{
		public ProtoPool<CriticalAttackMarkerComponent> CriticalAttackMarker;
		public ProtoPool<CriticalEffectComponent> CriticalEffect;
		public ProtoPool<CriticalEffectReadyComponent> CriticalEffectReady;
		public ProtoPool<EffectComponent> Effect;
	}
}