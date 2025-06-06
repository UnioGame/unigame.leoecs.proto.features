namespace UniGame.Ecs.Proto.Gameplay.ArmorResist.Systems
{
	using System;
	using Characteristics.ArmorResist.Aspects;
	using Damage.Aspects;
	using Damage.Components.Request;
	using GameEffects.DamageEffect.DamageTypes.Aspects;
	using LeoEcs.Shared.Extensions;
	using Leopotam.EcsProto;
	using Leopotam.EcsProto.QoL;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

	/// <summary>
	/// Recalculated damage value by armor resist.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class RecalculatedDamageArmorResistSystem : IProtoRunSystem
	{
		private ProtoWorld _world;
		private ArmorResistAspect _armorResistAspect;
		private DamageTypesAspect _damageTypesAspect;
		private DamageAspect _damageAspect;
		
		private ProtoIt _filter = It
			.Chain<ApplyDamageRequest>()
			.End();

		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var request = ref _damageAspect.ApplyDamage.Get(entity);
				if (!request.Effector.Unpack(_world, out var effectorEntity))
					continue;
				if (!_damageTypesAspect.PhysicsDamage.Has(effectorEntity))
					continue;
				if (!request.Destination.Unpack(_world, out var destinationEntity))
					continue;
				if (!_armorResistAspect.ArmorResist.Has(destinationEntity))
					continue;
				ref var armorResistComponent = ref _armorResistAspect.ArmorResist.Get(destinationEntity);
				var damage = request.Value;
				var resist = armorResistComponent.Value / 100f;
				request.Value = damage - damage * resist;
			}
		}
	}
}