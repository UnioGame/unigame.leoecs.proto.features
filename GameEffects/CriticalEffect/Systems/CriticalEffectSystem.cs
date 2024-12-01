namespace UniGame.Ecs.Proto.GameEffects.CriticalEffect.Systems
{
	using System;
	using Aspect;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class CriticalEffectSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private CriticalEffectAspect _aspect;

		private ProtoItExc _filter = It
			.Chain<CriticalEffectComponent>()
			.Exc<CriticalEffectReadyComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var effectComponent = ref _aspect.Effect.Get(entity);
				if (!effectComponent.Destination.Unpack(_world, out var destinationEntity))
					continue;
				_aspect.CriticalAttackMarker.GetOrAddComponent(destinationEntity);
				_aspect.CriticalEffectReady.Add(entity);
			}
		}
	}
}