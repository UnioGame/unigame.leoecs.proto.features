namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.CloseTemporaryUIAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class CloseTemporaryUIActionAspect : EcsAspect
	{
		public ProtoPool<CompletedCloseTemporaryUIActionComponent> CompletedCloseTemporaryUIAction;
	}
}