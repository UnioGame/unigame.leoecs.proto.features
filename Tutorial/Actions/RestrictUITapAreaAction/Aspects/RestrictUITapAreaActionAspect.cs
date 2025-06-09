namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using Triggers.ActionTrigger.Components;
	using LeoEcs.Bootstrap;
	
	[Serializable]
	public class RestrictUITapAreaActionAspect : EcsAspect
	{
		public ProtoPool<RestrictUITapAreaActionComponent> RestrictUITapAreaAction;
		public ProtoPool<ActivateRestrictUITapAreaComponent> ActivateRestrictUITapArea;
		public ProtoPool<RestrictUITapAreaActionReadyComponent> RestrictUITapAreaActionReady;
		public ProtoPool<RestrictUITapAreaComponent> RestrictUITapArea;
		public ProtoPool<RestrictUITapAreasComponent> RestrictUITapAreas;
		public ProtoPool<CompletedRestrictUITapAreaActionComponent> CompletedRestrictUITapAreaAction;
		public ProtoPool<CompletedRestrictUITapAreaComponent> CompletedRestrictUITapArea;
		public ProtoPool<ActionTriggerRequest> ActionTriggerRequest;
		public ProtoPool<CompletedRunRestrictActionsComponent> CompletedRunRestrictActions;
	}
}