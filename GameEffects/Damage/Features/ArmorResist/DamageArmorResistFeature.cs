namespace UniGame.Ecs.Proto.Gameplay.ArmorResist
{
	using Cysharp.Threading.Tasks;
	using Damage;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ECS Proto/Features/Damage/Armor Resist Feature", fileName = "Damage Armor Resist Feature")]
	public sealed class DamageArmorResistFeature : DamageSubFeature
	{
		public sealed override UniTask BeforeDamageSystem(IProtoSystems ecsSystems)
		{
			// recalculate damage by armor resist
			ecsSystems.Add(new RecalculatedDamageArmorResistSystem());
			
			return UniTask.CompletedTask;
		}
	}
}