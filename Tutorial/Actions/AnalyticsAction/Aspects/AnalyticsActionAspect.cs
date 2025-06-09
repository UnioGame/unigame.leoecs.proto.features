namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.AnalyticsAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class AnalyticsActionAspect : EcsAspect
	{
		public ProtoPool<AnalyticsActionComponent> AnalyticsAction;
		public ProtoPool<CompletedAnalyticsActionComponent> CompletedAnalyticsAction;
	}
}