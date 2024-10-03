namespace UniGame.Ecs.Proto.Ability.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Components.Requests;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
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
    public class ActivateAbilityByIdSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoWorld _world;

        private ProtoIt _abilityFilter= It
            .Chain<ActiveAbilityComponent>()
            .Inc<AbilityIdComponent>()
            .Inc<OwnerLinkComponent>()
            .End();
        
        private ProtoIt _requestFilter= It
            .Chain<ActivateAbilityByIdRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _requestFilter)
            {
                ref var request = ref _abilityAspect.ActivateAbilityByIdRequest.Get(requestEntity);
                if(!request.Target.Unpack(_world,out var targetEntity)) continue;

                foreach (var abilityEntity in _abilityFilter)
                {
                    ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(abilityEntity);
                    if (!ownerLinkComponent.Value.Equals(request.Target))
                    {
                        continue;
                    }
                    
                    ref var abilityId = ref _abilityAspect.AbilityId.Get(abilityEntity);
                    if (abilityId.AbilityId != request.AbilityId) continue;

                    ref var activateRequest = ref _abilityAspect.ActivateAbilityRequest.GetOrAddComponent(requestEntity);
                    activateRequest.Ability = _world.PackEntity(abilityEntity);
                    activateRequest.Target = request.Target;
                    
                    break;
                }
                
            }
        }
    }
}