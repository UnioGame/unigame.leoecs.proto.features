namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Run tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RunTutorialActionsSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private TutorialTriggerPointAspect _aspect;
		private EcsFilter _requestFilter;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_requestFilter = _world
				.Filter<RunTutorialActionsRequest>()
				.End();
		}

		public void Run()
		{
			foreach (var requestEntity in _requestFilter)
			{
				ref var requestComponent = ref _aspect.RunTutorialActionsRequest.Get(requestEntity);
				if (!requestComponent.Source.Unpack(_world, out var sourceEntity))
					continue;
				ref var actionsComponent = ref _aspect.TutorialActions.Get(sourceEntity);
				foreach (var action in actionsComponent.Actions)
				{
					var actionEntity = _world.NewEntity();
					
					sourceEntity.AddChild(actionEntity, _world);
					action.ComposeEntity(_world, actionEntity);
				}
			}
		}
	}
}