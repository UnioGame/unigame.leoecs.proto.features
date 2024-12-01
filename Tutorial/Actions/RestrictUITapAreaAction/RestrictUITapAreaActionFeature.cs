namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.RestrictUITapAreaAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Restrict UI Tap Area Action Feature", 
		fileName = "Restrict UI Tap Area Action Feature")]
	public class RestrictUITapAreaActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new InitializationRestrictUITapAreaActionSystem());
			ecsSystems.Add(new ProcessRestrictUITapAreaSystem());
			ecsSystems.Add(new SelectionNextTapAreaSystem());
			ecsSystems.Add(new RunRestrictAreaActionsSystem());
		}
	}
}