namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.HealingChampionAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Healing Champion Action", 
		fileName = "Healing Champion Action Feature")]
	public class HealingChampionActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Heals champion
			ecsSystems.Add(new HealingChampionActionSystem());
		}
	}
}