namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SetInHandAbilityRequestSystem : IProtoRunSystem
    {
        private AbilityAspect _abilityAspect;
        private ProtoWorld _world;
        
        private ProtoIt _filter = It
            .Chain<SetInHandAbilitySelfRequest>()
            .Inc<AbilityInHandLinkComponent>()
            .End();

        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var setInHand = ref _abilityAspect.SetInHandAbilitySelfRequest.Get(entity);
                if(!setInHand.Value.Unpack(_world,out var nextAbilityEntity))
                    continue;

                if(!_abilityAspect.Active.Has(nextAbilityEntity)) continue;
                
                _abilityAspect.ChangeInHandAbility(entity, nextAbilityEntity);
            }
        }
    }
}