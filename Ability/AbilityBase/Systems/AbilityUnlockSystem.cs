namespace UniGame.Ecs.Proto.Ability.Systems
{
	using System;
	using Aspects;
	using Components;
	using LeoEcs.Bootstrap.Runtime.Attributes;
	using Leopotam.EcsLite;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Shared.Extensions;


	/// <summary>
	/// Ability unlock. Wait for unlock event.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class AbilityUnlockSystem : IProtoInitSystem, IProtoRunSystem
	{
		private ProtoWorld _world;
		private EcsFilter _eventFilter;

		private AbilityAspect _abilityAspect;

		public void Init(IProtoSystems systems)
		{
			_world = systems.GetWorld();
			_eventFilter = _world.Filter<AbilityUnlockEvent>()
				.End();
		}

		public void Run()
		{
			foreach (var entity in _eventFilter)
			{
				ref var unlockEvent = ref _abilityAspect.AbilityUnlockEvent.Get(entity);
				if (!unlockEvent.Ability.Unpack(_world, out var abilityEntity))
					continue;
				if (_abilityAspect.AbilityUnlockComponent.Has(abilityEntity))
					continue;
				_abilityAspect.AbilityUnlockComponent.Add(abilityEntity);
			}
		}
	}
}