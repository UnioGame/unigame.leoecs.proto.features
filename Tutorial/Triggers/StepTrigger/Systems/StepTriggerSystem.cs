namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.StepTrigger.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;
	
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class StepTriggerSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private StepTriggerAspect _aspect;

		private ProtoItExc _filter = It
			.Chain<StepTriggerReadyComponent>()
			.Exc<CompletedStepTriggerComponent>()
			.End();
		
		private ProtoIt _startLevelFilter = It
			.Chain<TutorialReadyComponent>()
			.End();

		public void Run()
		{
			if (_startLevelFilter.IsEmpty()) return;
			
			foreach (var entity in _filter)
			{
				_aspect.CompletedStepTrigger.Add(entity);
				var requestEntity = _world.NewEntity();
				ref var request = ref _aspect.RunTutorialActionsRequest.Add(requestEntity);
				request.Source = entity.PackEntity(_world);
			}
		}
	}
}