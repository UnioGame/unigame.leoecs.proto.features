namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OpenWindowAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UniGame.LeoEcs.ViewSystem.Extensions;

	/// <summary>
	/// Opening selected window.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class OpenWindowActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private OpenWindowActionAspect _aspect;
		
		private ProtoItExc _filter = It
			.Chain<OpenWindowActionComponent>()
			.Exc<CompletedOpenWindowActionComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var openWindowActionComponent = ref _aspect.OpenWindowAction.Get(entity);
				var view = openWindowActionComponent.View;
				var layoutType = openWindowActionComponent.LayoutType;
				_world.MakeViewRequest(view, layoutType);
				_aspect.CompletedOpenWindowAction.Add(entity);
			}
		}
	}
}