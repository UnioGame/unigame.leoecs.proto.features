namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.FreezingTimeAction.Aspects
{
	using Components;
	using FreezingTime.Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	public class FreezingTimeActionAspect : EcsAspect
	{
		public ProtoWorld World;
		
		public ProtoPool<FreezingTimeActionComponent> FreezingTimeAction;
		public ProtoPool<FreezingTimeRequest> FreezingTimeRequest;
		public ProtoPool<CompletedFreezingTimeActionComponent> CompletedFreezingTimeAction;
	}
}