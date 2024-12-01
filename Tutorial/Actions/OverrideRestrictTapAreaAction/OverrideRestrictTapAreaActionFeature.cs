namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction
{
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using Tutorial.Abstracts;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Override Restrict UI Tap Area Action Feature", 
		fileName = "Override Restrict UI Tap Area Action Feature")]
	public class OverrideRestrictTapAreaActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new InitializationOverrideRestrictAreaActionSystem());
		}
	}
}