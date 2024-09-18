namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
	using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RunRestrictAreaActionsSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		
		private RestrictUITapAreaActionAspect _aspect;
		private OwnershipAspect _ownershipAspect;
		
		private EcsFilter _actionsFilter;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_actionsFilter = _world
				.Filter<RestrictUITapAreaComponent>()
				.Inc<ActivateRestrictUITapAreaComponent>()
				.Exc<CompletedRunRestrictActionsComponent>()
				.End();
		}

		public void Run()
		{
			foreach (var entity in _actionsFilter)
			{
				ref var restrictTapAreaComponent = ref _aspect.RestrictUITapArea.Get(entity);
				var actions = restrictTapAreaComponent.Value.Actions;
				foreach (var tutorialAction in actions)
				{
					var actionEntity = _world.NewEntity();
					tutorialAction.ComposeEntity(_world, actionEntity);
					
					entity.AddChild(actionEntity, _world);
				}
				_aspect.CompletedRunRestrictActions.Add(entity);
			}
		}
	}
}