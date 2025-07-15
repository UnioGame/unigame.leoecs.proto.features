namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.DistanceTrigger.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Core.Components;
	using UniGame.Proto.Ownership;
	using LeoEcs.Shared.Extensions;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using Unity.Mathematics;

	/// <summary>
	/// Sends request to run tutorial actions when champion is in trigger distance.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class TutorialTriggerDistanceSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		
		private DistanceTriggerAspect _aspect;
		private OwnershipAspect _ownershipAspect;
		
		private ProtoIt _startLevelFilter = It
			.Chain<TutorialReadyComponent>()
			.End();
		
		private ProtoIt _championFilter = It
			.Chain<ChampionComponent>()
			.End();
		
		private ProtoItExc _distanceTriggerPointFilter = It
			.Chain<DistanceTriggerPointComponent>()
			.Exc<CompletedDistanceTriggerPointComponent>()
			.End();

		public void Run()
		{
			if (_startLevelFilter.IsEmptySlow() || _championFilter.IsEmptySlow()) return;
			
			var championEntity = _championFilter.FirstSlow().Entity;
			ref var positionComponent = ref _aspect.Position.Get(championEntity);
			ref var position = ref positionComponent.Position;
			
			foreach (var triggerEntity in _distanceTriggerPointFilter)
			{
				ref var triggerOwnerLinkComponent = ref _ownershipAspect.OwnerLink.Get(triggerEntity);
				if (!triggerOwnerLinkComponent.Value.Unpack(_world, out var triggerOwnerEntity))
					continue;
				
				ref var triggerTransformComponent = ref _aspect.Position.Get(triggerOwnerEntity);
				var triggerTransform = triggerTransformComponent.Position;
				
				ref var distanceTriggerPointComponent = ref _aspect.DistanceTriggerPoint.Get(triggerEntity);
				
				var distance = math.distance(position, triggerTransform);
				if (distance > distanceTriggerPointComponent.TriggerDistance)
					continue;
				
				_aspect.CompletedDistanceTriggerPoint.Add(triggerEntity);
				
				var requestEntity = _world.NewEntity();
				ref var request = ref _aspect.RunTutorialActionsRequest.Add(requestEntity);
				request.Source = _world.PackEntity(triggerEntity);
			}
		}
	}
}