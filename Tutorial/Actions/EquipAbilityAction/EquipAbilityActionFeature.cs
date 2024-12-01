namespace UniGame.Ecs.Proto.Gameplay.Tutorial.Actions.EquipAbilityAction
{
	using Abstracts;
	using Cysharp.Threading.Tasks;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Tutorial/TutorialAction/Equip Ability Action Feature", 
		fileName = "Equip Ability Action Feature")]
	public class EquipAbilityActionFeature : TutorialFeature
	{
		public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
		{
			// Equip ability to champion
			ecsSystems.Add(new EquipAbilityActionSystem());
		}
	}
}