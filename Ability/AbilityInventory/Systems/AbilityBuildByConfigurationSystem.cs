namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Tools;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Assembling ability
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class AbilityBuildByConfigurationSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityTools;
        private ProtoWorld _world;
        
        private AbilityInventoryAspect _inventoryAspect;
        private AbilityAspect _ability;
        
        private ProtoItExc _filter= It
            .Chain<EquipAbilitySelfRequest>()
            .Inc<AbilityConfigurationComponent>()
            .Inc<AbilityEquipComponent>()
            .Inc<AbilityBuildingComponent>()
            .Exc<AbilityLoadingComponent>()
            .Exc<AbilityInventoryCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var abilityEntity in _filter)
            {
                ref var requestComponent = ref _inventoryAspect.Equip
                    .GetOrAddComponent(abilityEntity);
                ref var configurationDataComponent = ref _inventoryAspect.Configuration
                    .GetOrAddComponent(abilityEntity);

                var abilityConfiguration = configurationDataComponent.Value;

                var buildData = new AbilityBuildData()
                {
                    AbilityId = requestComponent.AbilityId,
                    Slot = requestComponent.AbilitySlot,
                    IsDefault = requestComponent.IsDefault,
                };

                _abilityTools.BuildAbility(abilityEntity,
                    ref requestComponent.Target,
                    abilityConfiguration,ref buildData);
                
                _inventoryAspect.Complete.Add(abilityEntity);
            }
        }
        
    }
}