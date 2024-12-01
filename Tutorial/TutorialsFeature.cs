namespace UniGame.Ecs.Proto.Gameplay.Tutorial
{
	using System.Collections.Generic;
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Sirenix.OdinInspector;
	using Systems;
	using UniGame.LeoEcs.Bootstrap.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial Feature", fileName = "Tutorial Feature")]
	public class TutorialsFeature : BaseLeoEcsFeature
	{
		[SerializeReference]
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
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