namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.AnalyticsAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Data;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.Context.Runtime.Extension;
	using UniGame.Core.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Analytics Action Feature", 
		fileName = "Analytics Action Feature")]
	public class AnalyticsActionFeature : TutorialFeature
	{
		[SerializeReference]
		public ITutorialAnalytics analytics;
		
		public override UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			var context = ecsSystems.GetShared<IContext>();
			var service = context.Get<ITutorialAnalytics>() ?? analytics;
			ecsSystems.Add(new SendAnalyticsActionSystem(service));
			
			return UniTask.CompletedTask;
		}
	}
}