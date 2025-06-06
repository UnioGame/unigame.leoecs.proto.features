namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.HealingChampionAction.Systems
{
	using System;
	using Aspects;
	using Characteristics.Health.Components;
	using Components;
	using Game.Code.Configuration.Runtime.Effects;
	using Game.Ecs.Core.Components;
	using GameEffects.HealingEffect;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UnityEngine;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.Shared.Extensions;

	/// <summary>
	/// Heals the champion
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class HealingChampionActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private HealingChampionActionAspect _aspect;
		private HealingEffectConfiguration _healingEffectConfiguration;

		private ProtoIt _championFilter = It
			.Chain<ChampionComponent>()
			.Inc<HealthComponent>()
			.End();
		
		private ProtoItExc _actionFilter = It
			.Chain<HealingChampionActionComponent>()
			.Exc<CompletedHealingChampionActionComponent>()
			.End();

		public void Run()
		{
			foreach (var actionEntity in _actionFilter)
			{
				if (_championFilter.IsEmptySlow()) continue;
				
				var championEntity = _championFilter.First().Entity;
				var healthComponent = _aspect.Healths.Get(championEntity);
				ref var healingActionComponent = ref _aspect.HealingChampionAction.Get(actionEntity);
				var currentHealth = healthComponent.Value;
				var maxHealth = healthComponent.MaxValue;
				
				var healOverMax = healingActionComponent.HealOverMax;
				var healDuration = healingActionComponent.HealDuration;
				var healPeriod = healingActionComponent.HealPeriod;
				
				_healingEffectConfiguration = new HealingEffectConfiguration()
				{
					duration = healDuration,
					periodicity = healPeriod,
					healingValue = 0.3f,
					targetType = TargetType.Target,
				};

				if(Mathf.Approximately(maxHealth,currentHealth)) continue;

				var healingEntity = _world.NewEntity();
                    
				var overrideMaxHealth = healOverMax + maxHealth;
				var difference = overrideMaxHealth - currentHealth;
				var healAtTick = healDuration / healPeriod;
				var healValue = difference / healAtTick;

				_healingEffectConfiguration.healingValue = healValue;
				_healingEffectConfiguration.duration = healDuration;
				_healingEffectConfiguration.ComposeEntity(_world,healingEntity);
                
				_aspect.ApplyEffectRequest.Add(healingEntity);
                
				ref var effectComponent = ref _aspect.Effects.Add(healingEntity);
				effectComponent.Destination = championEntity.PackEntity(_world);
				effectComponent.Source = healingEntity.PackEntity(_world);
				
				_aspect.CompletedHealingChampionAction.Add(actionEntity);
			}
		}
	}
}