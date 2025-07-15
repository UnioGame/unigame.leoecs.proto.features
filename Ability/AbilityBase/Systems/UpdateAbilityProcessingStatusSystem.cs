namespace UniGame.Ecs.Proto.Ability.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using UniGame.Proto.Ownership;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// mark ability ro as processing ability or not
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateAbilityProcessingStatusSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private ProtoIt _abilityRootFilter;
        private ProtoIt _abilityInUseFilter;
        
        private AbilityAspect _abilityAspect;
        private AbilityOwnerAspect _abilityOwnerAspect;
        private OwnershipAspect _ownershipAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _abilityRootFilter = _world
                .Filter<AbilityMapComponent>()
                .End();
            
            _abilityInUseFilter = _world
                .Filter<AbilityIdComponent>()
                .Inc<AbilityUsingComponent>()
                .Inc<ActiveAbilityComponent>()
                .Inc<OwnerLinkComponent>()
                .End();
        }

        public void Run()
        {
            foreach (var entity in _abilityRootFilter)
            {
                var targetAbility = -1;
                
                foreach (var abilityEntity in _abilityInUseFilter)
                {
                    ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityEntity);
                    if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                        continue;
                    
                    if (!ownerEntity.Equals(entity)) continue;

                    targetAbility = (int)abilityEntity;
                }

                if (targetAbility < 0)
                {
                    _abilityOwnerAspect.AbilityInProcessing.TryRemove(entity);
                    continue;
                }

                var targetAbilityEntity = (ProtoEntity)targetAbility;
                ref var slotComponent = ref _abilityAspect.Slot.Get(targetAbilityEntity);
                ref var progressComponent = ref _abilityOwnerAspect.AbilityInProcessing.GetOrAddComponent(entity);
                
                progressComponent.Ability = targetAbilityEntity.PackEntity(_world);
                progressComponent.Slot = slotComponent.SlotType;
                progressComponent.IsDefault = _abilityAspect.Default.Has(targetAbilityEntity);
            }
        }
    }
}