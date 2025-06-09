namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.ActionTrigger.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using Tutorial.Components;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class ActionTriggerAspect : EcsAspect
	{
		public ProtoPool<ActionTriggerComponent> ActionTrigger;
		public ProtoPool<ActionTriggerRequest> ActionTriggerRequest;
		public ProtoPool<CompletedActionTriggerComponent> CompletedActionTrigger;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}