namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Search ability in inventory
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public class EquipAbilityByIdToChampionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityInventoryAspect _inventoryAspect;

        private ProtoIt _filter= It
            .Chain<EquipAbilityIdToChampionRequest>()
            .End();
        
        private ProtoIt _championFilter= It
            .Chain<ChampionComponent>()
            .End();
        
        public void Run()
        {
            foreach (var request in _filter)
            {
                ref var requestComponent = ref _inventoryAspect.EquipToChampion.Get(request);
				
                foreach (var entity in _championFilter)
                {
                    ref var abilityEquipRequest = ref _inventoryAspect.EquipById
                        .GetOrAddComponent(request);
					
                    abilityEquipRequest.AbilityId = requestComponent.AbilityId;
                    abilityEquipRequest.AbilitySlot = requestComponent.AbilitySlot;
                    abilityEquipRequest.IsDefault = requestComponent.IsDefault;
                    abilityEquipRequest.Owner = _world.PackEntity(entity);
					
                    _inventoryAspect.EquipToChampion.Del(request);
                }
            }
        }
    }
}