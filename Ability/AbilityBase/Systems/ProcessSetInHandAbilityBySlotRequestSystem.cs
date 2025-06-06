namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessSetInHandAbilityBySlotRequestSystem : IProtoInitSystem, IProtoRunSystem
    {
        private AbilityAspect _abilityAspect;
        private ProtoIt _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<SetInHandAbilityBySlotSelfRequest>()
                .Inc<AbilityMapComponent>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var setInHand = ref _abilityAspect.SetInHandAbilityBySlotSelfRequest.Get(entity);
                var slot = setInHand.AbilityCellId;
                
                var abilityInSlot = _abilityAspect.GetAbilityBySlot(entity, slot);
                if(!abilityInSlot.Unpack(_world,out var abilityEntity))
                    continue;

                ref var request = ref _abilityAspect.SetInHandAbilitySelfRequest.GetOrAddComponent(entity);
                request.Value = abilityInSlot;
            }
        }
    }
}