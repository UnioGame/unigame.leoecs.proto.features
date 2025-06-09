namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.ShowAllUIAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class ShowAllUIActionAspect : EcsAspect
	{
		public ProtoPool<ShowAllUIActionComponent> ShowAllUIAction;
		public ProtoPool<ShowAllUIActionEvent> ShowAllUIActionEvent;
		public ProtoPool<CompletedShowAllUIComponent> CompletedShowAllUI;
	}
}