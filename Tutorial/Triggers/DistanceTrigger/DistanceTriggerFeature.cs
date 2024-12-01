namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.DistanceTrigger
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialTrigger/Distance Trigger Feature", 
		fileName = "Distance Trigger Feature")]
	public class DistanceTriggerFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Sends request to run tutorial actions when champion is in trigger distance.
			ecsSystems.Add(new TutorialTriggerDistanceSystem());
		}
	}
}