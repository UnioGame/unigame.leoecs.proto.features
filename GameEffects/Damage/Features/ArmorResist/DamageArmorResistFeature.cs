namespace UniGame.Ecs.Proto.Gameplay.ArmorResist
{
	using System;
	using Cysharp.Threading.Tasks;
	using Damage;
	using Leopotam.EcsProto;
	using Systems;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
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