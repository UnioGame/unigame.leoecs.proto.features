namespace UniGame.Ecs.Proto.Ability.Modifications
{
    using System;
    using Characteristics;
    using Characteristics.Cooldown.Components;
    using Common.Components;
    using Cooldown;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class AbilityCooldownModificationHandler : ModificationHandler
    {
        [SerializeField]
        public int abilityId;
        
        public CooldownType cooldownType = CooldownType.Cooldown;

        public sealed override Modification Create()
        {
            var cooldownValue = value;
            if (cooldownType == CooldownType.Speed && !isPercent)
                cooldownValue = 1.0f / cooldownValue;
            
            var modificationValue = new Modification(cooldownValue, isPercent, allowedSummation,isMaxLimitModification);
            return modificationValue;
        }

        public override void AddModification(ProtoWorld world,ProtoEntity source, ProtoEntity destinationEntity)
        {
            var abilityMapPool = world.GetPool<AbilityMapComponent>();
            if(!abilityMapPool.Has(destinationEntity))
                return;

            ref var abilityMap = ref abilityMapPool.Get(destinationEntity); 
            var abilitiesCount = abilityMap.Abilities.Count;
            if(abilityId < 0 || abilityId >= abilitiesCount)
                return;

            abilityMap.AbilitySlots.TryGetValue(abilityId, out var ability);
            if(!ability.Unpack(world, out var abilityEntity))
                return;
            
            var baseCooldownPool = world.GetPool<BaseCooldownComponent>();
            if(!baseCooldownPool.Has(abilityEntity))
                return;

            ref var baseCooldown = ref baseCooldownPool.Get(abilityEntity);
            baseCooldown.Modifications.AddModification(Modification);

            world.GetOrAddComponent<RecalculateCooldownSelfRequest>(abilityEntity);
        }

        public override void RemoveModification(ProtoWorld world,ProtoEntity source, ProtoEntity destinationEntity)
        {
            var abilityMapPool = world.GetPool<AbilityMapComponent>();
            if(!abilityMapPool.Has(destinationEntity))
                return;

            ref var abilityMap = ref abilityMapPool.Get(destinationEntity);
            var abilitiesCount = abilityMap.Abilities.Count;
            if(abilityId < 0 || abilityId >= abilitiesCount)
                return;
            
            abilityMap.AbilitySlots.TryGetValue(abilityId, out var ability);
            if(!ability.Unpack(world, out var abilityEntity))
                return;
            
            var baseCooldownPool = world.GetPool<BaseCooldownComponent>();
            if(!baseCooldownPool.Has(abilityEntity))
                return;

            ref var baseCooldown = ref baseCooldownPool.Get(abilityEntity);
            baseCooldown.Modifications.RemoveModification(Modification);
            
            var requestPool = world.GetPool<RecalculateCooldownSelfRequest>();
            if (!requestPool.Has(abilityEntity))
                requestPool.Add(abilityEntity);
        }
    }
}