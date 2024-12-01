namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.CloseTemporaryUIAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.ViewSystem.Components;

	/// <summary>
	/// Close temporary UI. Send event to close temporary UI.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class CloseTemporaryUIActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private CloseTemporaryUIActionAspect _aspect;

		private ProtoItExc _filter = It
			.Chain<CloseTemporaryUIActionComponent>()
			.Exc<CompletedCloseTemporaryUIActionComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				var request = _world.NewEntity();
				_world.AddComponent<CloseAllViewsRequest>(request);
				_aspect.CompletedCloseTemporaryUIAction.Add(entity);
			}
		}
	}
}