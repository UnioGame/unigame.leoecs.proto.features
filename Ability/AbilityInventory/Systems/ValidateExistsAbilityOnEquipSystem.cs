namespace UniGame.Ecs.Proto.AbilityInventory.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Common.Components;
    using Aspects;
    using Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// if ability with same id already exists in inventory - remove it
    /// </summary>
#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ValidateExistsAbilityOnEquipSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityTools;
        private AbilityInventoryAspect _inventoryAspect;
        private ProtoWorld _world;
        
        private ProtoItExc _filterRequest= It
            .Chain<EquipAbilitySelfRequest>()
            .Inc<AbilityIdComponent>()
            .Inc<OwnerLinkComponent>()
            .Exc<AbilityValidationFailedComponent>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _filterRequest)
            {
                ref var requestComponent = ref _inventoryAspect.Equip.Get(requestEntity);
                var abilityId = requestComponent.AbilityId;

                if (!requestComponent.Target.Unpack(_world, out var targetEntity))
                {
                    _inventoryAspect.Failed.GetOrAddComponent(requestEntity);
                    continue;
                }

                var existsAbility = _abilityTools.GetExistsAbility(abilityId, ref requestComponent.Target);
                if (existsAbility <= 0) continue;
                
                _inventoryAspect.Failed.GetOrAddComponent(requestEntity);
                continue;
            }
        }
    }
}