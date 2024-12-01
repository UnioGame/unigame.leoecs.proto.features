namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.ShowAllUIAction.Systems
{
	using System;
	using Aspects;
	using Components;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Show all UI. Send event to show all UI.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class ShowAllUIActionSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private ShowAllUIActionAspect _aspect;
		
		private ProtoItExc _filter = It
			.Chain<ShowAllUIActionComponent>()
			.Exc<CompletedShowAllUIComponent>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				var eventEntity = _world.NewEntity();
				_aspect.ShowAllUIActionEvent.Add(eventEntity);
				_aspect.CompletedShowAllUI.Add(entity);
			}
		}
	}
}