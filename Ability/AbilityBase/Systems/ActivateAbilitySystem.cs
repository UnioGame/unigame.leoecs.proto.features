namespace UniGame.Ecs.Proto.Ability.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    /// <summary>
    /// Activate ability by id
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateAbilitySystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoIt _requestFilter= It
            .Chain<ActivateAbilityRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var request = ref _abilityAspect.ActivateAbilityOnTarget.Get(requestEntity);
                if(!request.Target.Unpack(_world,out var targetEntity))
                    continue;

                if(!request.Ability.Unpack(_world,out var targetAbilityEntity))
                    continue;

                foreach (var abilityEntity in _abilityAspect.ActiveAbilityFilter)
                {
                    ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityEntity);
                    if(!ownerLinkComponent.Value.Unpack(_world,out _)) continue;
                    if (!ownerLinkComponent.Value.Equals(request.Target)) continue;
                    
                    if (!abilityEntity.Equals(targetAbilityEntity)) continue;
                    
                    _abilityAspect.ActivateAbility(targetEntity,abilityEntity);
                    
                    break;
                }
                
            }
        }
    }
}