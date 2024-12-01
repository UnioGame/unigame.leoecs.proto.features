namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OpenWindowAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Open Window Action Feature", 
		fileName = "Open Window Action Feature")]
	public class OpenWindowActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Open window
			ecsSystems.Add(new OpenWindowActionSystem());
		}
	}
}