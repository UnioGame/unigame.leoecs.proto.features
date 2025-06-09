namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;
	
	[Serializable]
	public class OverrideRestrictTapAreaActionAspect : EcsAspect
	{
		public ProtoPool<OverrideRestrictTapAreaActionComponent> OverrideRestrictTapAreaAction;
		public ProtoPool<OverrideRestrictTapAreaActionReadyComponent> OverrideRestrictTapAreaActionReady;
	}
}