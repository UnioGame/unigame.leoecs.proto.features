namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Triggers.ActionTrigger
{
	using Abstracts;
	using Components;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialTrigger/Action Trigger Feature", 
		fileName = "Action Trigger Feature")]
	public class ActionTriggerFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Sends request to execute action. Await ActionTriggerRequest.
			ecsSystems.Add(new ActionTriggerSystem());
			ecsSystems.DelHere<ActionTriggerRequest>();
		}
	}
}