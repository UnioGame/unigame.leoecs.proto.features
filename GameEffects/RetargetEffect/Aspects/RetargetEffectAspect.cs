namespace UniGame.Ecs.Proto.GameEffects.RetargetEffect.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	/// <summary>
	/// Retarget effect aspect
	/// </summary>
	[Serializable]
	public class RetargetEffectAspect : EcsAspect
	{
		// Stores the duration of the retarget effect
		public ProtoPool<RetargetComponent> RetargetComponent;
		// Marks the target as untargetable
		//public ProtoPool<UntargetableComponent> UntargetableComponent;
	}
}