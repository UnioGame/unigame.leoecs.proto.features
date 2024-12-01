namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.CloseTemporaryUIAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Close Temporary UI Action Feature", 
		fileName = "Close Temporary UI Action Feature")]
	public class CloseTemporaryUIActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new CloseTemporaryUIActionSystem());
		}
	}
}