namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.DistanceTrigger.Aspects
{
	using UniGame.Ecs.Proto.Gameplay.Tutorial.Components;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	public class DistanceTriggerAspect : EcsAspect
	{
		public ProtoWorld World;
		
		public ProtoPool<DistanceTriggerPointComponent> DistanceTriggerPoint;
		public ProtoPool<CompletedDistanceTriggerPointComponent> CompletedDistanceTriggerPoint;
		public ProtoPool<TransformPositionComponent> Position;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
	}
}