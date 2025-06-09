namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.SavePrefAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsProto;
	using LeoEcs.Bootstrap;

	[Serializable]
	public class SavePrefAspect : EcsAspect
	{
		public ProtoPool<SavePrefComponent> SavePref;
		public ProtoPool<CompletedSavePrefComponent> CompletedSavePref;
	}
}