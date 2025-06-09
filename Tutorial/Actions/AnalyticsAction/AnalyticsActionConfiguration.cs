namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.AnalyticsAction
{
	using System;
	using Abstracts;
	using ActionTools;
	using Components;
	using Leopotam.EcsProto;

	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif
	
	[Serializable]
	public class AnalyticsActionConfiguration : ITutorialAction
	{
		#region Inspector

#if ODIN_INSPECTOR
		[TitleGroup("Event Data")]
#endif

		[SerializeReference]
		public TutorialKey stepName;
        

		#endregion
		
		public void ComposeEntity(ProtoWorld world, ProtoEntity entity)
		{
			ref var request = ref world.AddComponent<AnalyticsActionComponent>(entity);
			request.stepName = stepName;
		}
	}
}