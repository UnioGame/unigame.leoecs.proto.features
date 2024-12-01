namespace UniGame.Ecs.Proto.GameEffects.BlockAutoAttackEffect.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Common.Components;
    using AbilityInventory.Components;
    using Game.Ecs.Time.Service;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Remove block auto attack effect system.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveBlockAutoAttackEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;

        private ProtoItExc _abilityFilter = It
            .Chain<AbilityPauseComponent>()
            .Inc<DefaultAbilityComponent>()
            .Exc<AbilityMetaComponent>()
            .End();

        public void Run()
        {
            foreach (var abilityEntity in _abilityFilter)
            {
                ref var pauseAbilityComponent = ref _abilityAspect.AbilityPauseComponent.Get(abilityEntity);
                if (pauseAbilityComponent.Duration > GameTime.Time)
                    continue;
                _abilityAspect.AbilityPauseComponent.Del(abilityEntity);
            }
        }
    }
}