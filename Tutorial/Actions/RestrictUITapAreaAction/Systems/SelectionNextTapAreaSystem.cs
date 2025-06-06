namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using LeoEcs.Shared.Extensions;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class SelectionNextTapAreaSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private RestrictUITapAreaActionAspect _aspect;

		private ProtoIt _activeRestrictTapAreaFilter = It
			.Chain<RestrictUITapAreaComponent>()
			.Inc<ActivateRestrictUITapAreaComponent>()
			.Inc<CompletedRestrictUITapAreaComponent>()
			.End();
		
		private ProtoItExc _restrictTapAreasFilter = It
			.Chain<RestrictUITapAreasComponent>()
			.Exc<CompletedRestrictUITapAreaActionComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _activeRestrictTapAreaFilter)
			{
				if (_restrictTapAreasFilter.IsEmpty()) continue;
				
				var areasEntity = _restrictTapAreasFilter.First().Entity;
				ref var areasActionComponent = ref _aspect.RestrictUITapAreaAction.Get(areasEntity);
				ref var areasComponent = ref _aspect.RestrictUITapAreas.Get(areasEntity);		
				_aspect.ActivateRestrictUITapArea.Del(entity);
				
				if (areasComponent.Value.Count == 0)
				{
					_aspect.CompletedRestrictUITapAreaAction.Add(areasEntity);
					var requestEntity = _world.NewEntity();
					ref var request = ref _aspect.ActionTriggerRequest.Add(requestEntity);
					request.ActionId = areasActionComponent.ActionId;
					continue;
				}
				
				var area = areasComponent.Value.Dequeue();
				if (!area.Unpack(_world, out var areaEntity))
					continue;
				_aspect.ActivateRestrictUITapArea.Add(areaEntity);
			}
		}
	}
}