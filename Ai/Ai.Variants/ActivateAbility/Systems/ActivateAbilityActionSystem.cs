namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Game.Code.Ai.ActivateAbility.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Ability.Aspects;
    using UniGame.Ecs.Proto.AI.Abstract;
    using UniGame.Ecs.Proto.AI.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Действие ИИ «применить способность»: если способность готова (активна и кулдаун прошёл), применяет
    /// её по выбранной цели.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ActivateAbilityActionSystem : IAiActionSystem
    {
        private AbilityAspect _abilityTools;
        private ProtoWorld _world;
        
        private AbilityAiActionAspect _aiActionAspect;

        private ProtoIt _filter= It
            .Chain<AiAgentComponent>()
            .Inc<ActivateAbilityActionComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var activateAbilityComponent = ref _aiActionAspect.ActivateAbilityAction.Get(entity);
                if (!activateAbilityComponent.Ability.Unpack(_world, out var abilityEntity))
                    continue;

                if (!_abilityTools.Active.Has(abilityEntity) || !_abilityTools.IsAbilityCooldownPassed(abilityEntity))
                    continue;

                _abilityTools.ActivateAbility(entity, abilityEntity, activateAbilityComponent.Target);
            }
        }
    }
}