namespace UniGame.Ecs.Proto.Effects.Systems
{
	using System;
	using Aspects;
	using Components;
	using Game.Ecs.Time.Service;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UnityEngine;

	/// <summary>
	/// Delayed effect system. Trigger effect after delay
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class DelayedEffectSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private EffectAspect _effectAspect;

		private ProtoItExc _filter = It
			.Chain<DelayedEffectComponent>()
			.Exc<EffectDurationComponent>()
			.Exc<EffectPeriodicityComponent>()
			.Exc<CompletedDelayedEffectComponent>()
			.End();
		
		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var delayedEffect = ref _effectAspect.Delayed.Get(entity);
				var nextApplyingTime = delayedEffect.LastApplyingTime + delayedEffect.Delay;
				if(GameTime.Time < nextApplyingTime && !Mathf.Approximately(nextApplyingTime, GameTime.Time))
					continue;
				delayedEffect.Configuration.ComposeEntity(_world, entity);
				_effectAspect.CompletedDelayed.Add(entity);
			}
		}
	}
}