namespace UniGame.Ecs.Proto.Ability.Converters
{
    using System;
    using UniCore.Runtime.ProfilerTools;
    using System.Collections.Generic;
    using Common.Components;
    using Components;
    using Game.Code.Configuration.Runtime.Ability;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;
    using UnityEngine;

    /// <summary>
    /// Gives the entity abilities straight from AbilityConfiguration assets, without inventory or catalog.
    /// Each entry becomes an EquipAbilityConfigurationSelfRequest handled by EquipAbilityConfigurationSystem.
    /// </summary>
    [Serializable]
    public class AbilityConfigurationConverter : LeoEcsConverter
    {
        public List<AbilitySetup> abilities = new();

        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            ref var map = ref world.GetOrAddComponent<AbilityMapComponent>(entity);
            world.GetOrAddComponent<AbilityInHandLinkComponent>(entity);
            ref var defaultSlot = ref world.GetOrAddComponent<DefaultAbilityTargetSlotComponent>(entity);

            var capacity = Mathf.Max(4, abilities.Count);
            if (!map.AbilitySlots.IsCreated)
                map.AbilitySlots = new NativeHashMap<int, ProtoPackedEntity>(capacity, Allocator.Persistent);
            if (!map.Abilities.IsCreated)
                map.Abilities = new NativeHashSet<ProtoPackedEntity>(capacity, Allocator.Persistent);

            var owner = entity.PackEntity(world);

            foreach (var setup in abilities)
            {
                if (setup.configuration == null)
                {
                    GameLog.LogError($"[Ability] {target.name}: empty ability configuration in {nameof(AbilityConfigurationConverter)}.", target);
                    continue;
                }

                ref var request = ref world.AddComponent<EquipAbilityConfigurationSelfRequest>(world.NewEntity());
                request.Owner = owner;
                request.Configuration = setup.configuration;
                request.Slot = setup.slot;
                request.IsDefault = setup.isDefault;

                if (setup.isDefault)
                    defaultSlot.Value = setup.slot;
            }
        }
    }

    [Serializable]
    public struct AbilitySetup
    {
        public AbilityConfiguration configuration;
        public int slot;
        public bool isDefault;
    }
}