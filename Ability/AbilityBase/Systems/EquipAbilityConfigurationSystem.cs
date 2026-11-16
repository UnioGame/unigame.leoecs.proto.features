namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Tools;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Выдаёт способность прямо из конфига: собирает её по шагам, кладёт владельцу в слот и делает готовой.
    /// Основную сразу берёт в руку.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class EquipAbilityConfigurationSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;

        // Abilities equipped from a configuration have no catalog id; negative ids never collide with catalog ones.
        private int _nextAbilityId = -1;

        private ProtoIt _requestFilter = It
            .Chain<EquipAbilityConfigurationSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var abilityEntity in _requestFilter)
            {
                ref var request = ref _abilityOwnerAspect.EquipConfiguration.Get(abilityEntity);

                if (request.Configuration == null ||
                    !request.Owner.Unpack(_world, out var ownerEntity) ||
                    !_abilityOwnerAspect.AbilityMap.Has(ownerEntity))
                {
                    GameLog.LogError("[Ability] Cannot equip ability: no configuration, owner is dead or has no AbilityMapComponent.");
                    _world.DelEntity(abilityEntity);
                    continue;
                }

                var buildData = new AbilityBuildData
                {
                    AbilityId = _nextAbilityId--,
                    Slot = request.Slot,
                    IsDefault = request.IsDefault,
                };

                _abilityAspect.BuildAbility(abilityEntity, ref request.Owner, request.Configuration, ref buildData);

                Register(ownerEntity, abilityEntity, request.Slot);
                _abilityAspect.Active.Add(abilityEntity);

                if (!request.IsDefault)
                    continue;

                _abilityAspect.Default.GetOrAddComponent(abilityEntity);
                if (_abilityOwnerAspect.AbilityInHandLink.Has(ownerEntity))
                    _abilityAspect.ChangeInHandAbility(ownerEntity, abilityEntity);
            }
        }

        private void Register(ProtoEntity ownerEntity, ProtoEntity abilityEntity, int slot)
        {
            ref var map = ref _abilityOwnerAspect.AbilityMap.Get(ownerEntity);
            var packedAbility = _world.PackEntity(abilityEntity);

            if (slot >= 0)
            {
                if (map.AbilitySlots.TryGetValue(slot, out var previous))
                {
                    map.Abilities.Remove(previous);
                    if (previous.Unpack(_world, out var previousEntity))
                        _world.DelEntity(previousEntity);
                }

                map.AbilitySlots[slot] = packedAbility;
            }

            map.Abilities.Add(packedAbility);
        }
    }
}