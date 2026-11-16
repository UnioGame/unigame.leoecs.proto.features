namespace Game.Code.Ai.ActivateAbility
{
    using System;
    using Ecs.Core.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Ability.Aspects;
    using UniGame.Ecs.Proto.AI.Abstract;
    using UniGame.Ecs.Proto.AI.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    /// <summary>
    /// AI action: applies the planned ability to the planned target when the ability is ready (Active and cooldown passed).
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    [Serializable]
    public sealed class ActivateAbilityActionSystem : IAiActionSystem
    {
        private AbilityAspect _abilityTools;
        private ProtoWorld _world;
        
        private ProtoPool<ActivateAbilityActionComponent> _activateAbilityPool;

        private ProtoIt _filter= It
            .Chain<AiAgentComponent>()
            .Inc<ActivateAbilityActionComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var activateAbilityComponent = ref _activateAbilityPool.Get(entity);
                if (!activateAbilityComponent.Ability.Unpack(_world, out var abilityEntity))
                    continue;

                if (!_abilityTools.Active.Has(abilityEntity) || !_abilityTools.IsAbilityCooldownPassed(abilityEntity))
                    continue;

                _abilityTools.ActivateAbility(entity, abilityEntity, activateAbilityComponent.Target);
            }
        }
    }
}