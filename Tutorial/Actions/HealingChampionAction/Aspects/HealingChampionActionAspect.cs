namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.HealingChampionAction.Aspects
{
	using System;
	using Characteristics.Health;
	using Characteristics.Health.Components;
	using Components;
	using Effects.Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;
	
	[Serializable]
	public class HealingChampionActionAspect : EcsAspect
	{
		public ProtoPool<HealingChampionActionComponent> HealingChampionAction;
		public ProtoPool<HealthComponent> Healths;
		public ProtoPool<EffectComponent> Effects;
		public ProtoPool<ApplyEffectSelfRequest> ApplyEffectRequest;
		public ProtoPool<CompletedHealingChampionActionComponent> CompletedHealingChampionAction;
	}
}