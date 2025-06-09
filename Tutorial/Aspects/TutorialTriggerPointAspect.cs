namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Aspects
{
	using System;
	using Components;
	using Game.Ecs.Core.Components;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Components;
	using LeoEcs.Bootstrap;

    [Serializable]
	public class TutorialTriggerPointAspect : EcsAspect
	{
		public ProtoWorld World;
        
		public ProtoPool<TransformComponent> Transform;
		public ProtoPool<RunTutorialActionsRequest> RunTutorialActionsRequest;
		public ProtoPool<TutorialActionsComponent> TutorialActions;
		public ProtoPool<DelayedTutorialComponent> DelayedTutorial;
		public ProtoPool<CompletedDelayedTutorialComponent> CompletedDelayedTutorial;
	}
}