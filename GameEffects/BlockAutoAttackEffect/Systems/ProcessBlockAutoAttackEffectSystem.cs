namespace UniGame.Ecs.Proto.GameEffects.BlockAutoAttackEffect.Systems
{
	using Ability.Aspects;
	using Components;
	using Effects.Aspects;
	using Game.Ecs.Time.Service;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Add an empty target to an ability
	/// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[ECSDI]
	public class ProcessBlockAutoAttackEffectSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private AbilityAspect _abilityAspect;
		private AbilityOwnerAspect _abilityOwnerAspect;
		private EffectAspect _effectAspect;
		
		private ProtoPool<BlockAutoAttackEffectReadyComponent> _blockAttackEffectReadyPool;
		private ProtoPool<BlockAutoAttackEffectComponent> _blockAttackEffectPool;

		private ProtoItExc _filter= It
			.Chain<BlockAutoAttackEffectComponent>()
			.Exc<BlockAutoAttackEffectReadyComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var effectComponent = ref _effectAspect.Effect.Get(entity);
				if (!effectComponent.Destination.Unpack(_world, out var target))
					continue;

				var abilityEntity = _abilityAspect.TryGetAbility(target, 0);
				if ((int)abilityEntity < 0) continue;
				
				_blockAttackEffectReadyPool.Add(entity);
				ref var blockAttackEffectComponent = ref _blockAttackEffectPool.Get(entity);
				
				ref var pauseAbilityComponent = ref _abilityAspect.Pause.GetOrAddComponent(abilityEntity);
				pauseAbilityComponent.Duration = GameTime.Time + blockAttackEffectComponent.Duration;
			}
		}
	}
}