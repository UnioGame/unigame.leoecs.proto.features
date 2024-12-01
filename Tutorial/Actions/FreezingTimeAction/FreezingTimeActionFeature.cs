namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.FreezingTimeAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Freezing Time Action Feature", 
		fileName = "Freezing Time Action Feature")]
	public class FreezingTimeActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Freezes time
			ecsSystems.Add(new FreezingTimeActionSystem());
		}
	}
}