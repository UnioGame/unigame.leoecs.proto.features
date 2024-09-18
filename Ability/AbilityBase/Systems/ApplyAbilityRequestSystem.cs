namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
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
    public sealed class ApplyAbilityRequestSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private AbilityAspect _abilityAspect;
        private AbilityOwnerAspect _ownerAspect;
        private OwnershipAspect _ownershipAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<ApplyAbilitySelfRequest>()
                .Inc<AbilityInHandLinkComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _ownerAspect.ApplyAbility.Get(entity);
                
                var abilityEntity = request.Value;
                if(!abilityEntity.Unpack(_world,out var abilityValueEntity))
                    continue;

                if (!_ownershipAspect.OwnerLink.Has(abilityValueEntity))
                {
                    continue;
                }
                
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityValueEntity);
                if(!ownerLinkComponent.Value.Unpack(_world,out var ownerEntity))
                    continue;
                
                if(!ownerEntity.Equals(entity)) continue;
                
                ref var inHandLink = ref _ownerAspect.AbilityInHandLink.Get(entity);
                if(!inHandLink.AbilityEntity.Equals(abilityEntity))
                    continue;

                _abilityAspect.Validate.TryAdd(ref abilityEntity);
            }
        }
    }
}