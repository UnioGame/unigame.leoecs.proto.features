namespace UniGame.Ecs.Proto.Gameplay.FreezingTime.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	/// <summary>
	/// Freezing time aspect
	/// </summary>
	[Serializable]
	public class FreezingTimeAspect : EcsAspect
	{
		// Says that time should be un/frozen.
		public ProtoPool<FreezingTimeRequest> freezingTimeRequest;
		
		// Says that un/freezing time completed.
		public ProtoPool<FreezingTimeCompletedEvent> freezingTimeCompletedEvent;
	}
}