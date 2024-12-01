namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Systems
{
	using System;
	using Aspects;
	using Components;
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
	public class InitializationOverrideRestrictAreaActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private OverrideRestrictTapAreaActionAspect _aspect;

		private ProtoItExc _filter = It
			.Chain<OverrideRestrictTapAreaActionComponent>()
			.Exc<OverrideRestrictTapAreaActionReadyComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var restrictTapAreaComponent = ref _aspect.OverrideRestrictTapAreaAction.Get(entity);
				restrictTapAreaComponent.Value.ComposeEntity(_world, entity);
				_aspect.OverrideRestrictTapAreaActionReady.Add(entity);
			}
		}
	}
}