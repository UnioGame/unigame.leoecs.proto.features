namespace UniGame.Ecs.Proto.Characteristics.AttackSpeed.Converters
{
    using System;
    using Components;
    using Cooldown;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Base.Components.Requests;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// Converts attack speed data and applies it to the target game object in the ECS world.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AttackSpeedConverter : LeoEcsConverter,ICharacteristicConverter
    {
        public CooldownType cooldownType = CooldownType.Speed;
        public int abilitySlotId = 0;
        
        public float attackSpeed;
        public float minLimitValue = 0;
        public float maxLimitValue = 5;
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var createCharacteristicRequest = ref world.GetOrAddComponent<CreateCharacteristicRequest<AttackSpeedComponent>>(entity);
            createCharacteristicRequest.Value = attackSpeed;
            createCharacteristicRequest.MaxValue = maxLimitValue;
            createCharacteristicRequest.MinValue = minLimitValue;
            createCharacteristicRequest.Owner = entity.PackEntity(world);

            ref var attackSpeedComponent = ref world.GetOrAddComponent<AttackSpeedComponent>(entity);
            ref var attackAbilityIdComponent = ref world.GetOrAddComponent<AttackAbilityIdComponent>(entity);
            ref var attackSpeedCooldownTypeComponent = ref world.GetOrAddComponent<AttackSpeedCooldownTypeComponent>(entity);
            attackSpeedCooldownTypeComponent.Value = cooldownType;
            attackAbilityIdComponent.Value = abilitySlotId;
        }
    }
}
