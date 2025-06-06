namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    /// <summary>
    /// initialize ui equip view with link to its data
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DiscardSetInHandWhileExecutingAbilitySystem : IProtoRunSystem
    {
        private AbilityAspect _abilityAspect;
        
        private ProtoWorld _world;
        private AbilityOwnerAspect _abilityOwnerAspect;

        private ProtoIt _filter = It
            .Chain<SetInHandAbilitySelfRequest>()
            .Inc<AbilityMapComponent>()
            .Inc<AbilityInHandLinkComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                var executingAbility = _abilityAspect.GetNonDefaultAbilityInUse(entity);
                if(!executingAbility.Unpack(_world,out var ability)) continue;
                _abilityOwnerAspect.SetInHandAbility.Del(entity);
            }
        }
    }
}