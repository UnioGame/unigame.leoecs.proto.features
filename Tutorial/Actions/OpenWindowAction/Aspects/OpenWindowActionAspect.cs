namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OpenWindowAction.Aspects
{
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	public class OpenWindowActionAspect : EcsAspect
	{
		public ProtoPool<OpenWindowActionComponent> OpenWindowAction;
		public ProtoPool<CompletedOpenWindowActionComponent> CompletedOpenWindowAction;
	}
}