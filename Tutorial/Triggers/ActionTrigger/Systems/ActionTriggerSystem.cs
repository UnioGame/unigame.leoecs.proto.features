namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.ActionTrigger.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Tutorial.Components;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Sends request to run tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class ActionTriggerSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private ActionTriggerAspect _aspect;

		private ProtoIt _startLevelFilter = It
			.Chain<TutorialReadyComponent>()
			.End();
		
		private ProtoIt _requestFilter = It
			.Chain<ActionTriggerRequest>()
			.End();
		
		private ProtoItExc _actionTriggerFilter = It
			.Chain<ActionTriggerComponent>()
			.Exc<CompletedActionTriggerComponent>()
			.End();

		public void Run()
		{
			if (_startLevelFilter.IsEmpty()) return;
			
			foreach (var entity in _requestFilter)
			{
				ref var request = ref _aspect.ActionTriggerRequest.Get(entity);
				foreach (var actionTriggerEntity in _actionTriggerFilter)
				{
					ref var actionTrigger = ref _aspect.ActionTrigger.Get(actionTriggerEntity);
					var actionId = actionTrigger.ActionId.ToString();
					if (!actionId.Equals(request.ActionId)) 
						continue;
					_aspect.CompletedActionTrigger.Add(actionTriggerEntity);
					
					var requestEntity = _world.NewEntity();
					ref var runTutorialRequest = ref _aspect.RunTutorialActionsRequest.Add(requestEntity);
					runTutorialRequest.Source = _world.PackEntity(actionTriggerEntity);
				}
			}
		}
	}
}