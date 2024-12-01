namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.StepTrigger
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialTrigger/Step Trigger Feature", 
		fileName = "Step Trigger Feature")]
	public class StepTriggerFeature : TutorialFeature
	{
		public override UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			ecsSystems.Add(new StepTriggerSystem());
			
			return UniTask.CompletedTask;
		}
	}
}