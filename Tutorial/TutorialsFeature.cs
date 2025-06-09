namespace UniGame.Ecs.Proto.Gameplay.Tutorial
{
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	
#if ODIN_INSPECTOR
	using Sirenix.OdinInspector;
#endif

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial Feature")]
	public class TutorialsFeature : BaseLeoEcsFeature
	{
#if ODIN_INSPECTOR
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
#endif
		[SerializeReference]
		public List<TutorialFeature> tutorialFeatures = new List<TutorialFeature>();
		
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			foreach (var feature in tutorialFeatures)
				await feature.InitializeAsync(ecsSystems);
			
			// Run tutorial actions. Await RunTutorialActionsRequest.
			ecsSystems.Add(new RunTutorialActionsSystem());
			// Delayed tutorial system. After delay compose tutorial actions/triggers.
			ecsSystems.Add(new DelayedTutorialSystem());
			ecsSystems.DelHere<RunTutorialActionsRequest>();
		}
	}
}