namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.StepTrigger.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using Tutorial.Components;
	using LeoEcs.Bootstrap;

	/// <summary>
	/// tutorial trigger aspect
	/// </summary>
	[Serializable]
	public class StepTriggerAspect : EcsAspect
	{
		public ProtoPool<StepTriggerComponent> StepTrigger;
		public ProtoPool<StepTriggerReadyComponent> StepTriggerReady;
		public ProtoPool<CompletedStepTriggerComponent> CompletedStepTrigger;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}