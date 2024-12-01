namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.ShowAllUIAction
{
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Show All UI Action Feature", 
		fileName = "Show All UI Action Feature")]
	public class ShowAllUIActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.DelHere<ShowAllUIActionEvent>();
			// Send event to show all UI
			ecsSystems.Add(new ShowAllUIActionSystem());
		}
	}
}