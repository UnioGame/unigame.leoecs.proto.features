namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Time.Service;
    using UniGame.Proto.Ownership;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.FakeTimeline.Aspects;
    using UniGame.LeoEcs.Bootstrap;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Timer.Components;

    /// <summary>
    /// Применяет способность: создаёт её таймлайн с целями, снимает готовность, перезапускает кулдаун и
    /// помечает «используется». Пока таймлайн играет, повторно не применяется.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ApplyAbilitySystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoWorld _world;
        
        private AbilityAspect _abilityAspect;
        private OwnershipAspect _ownershipAspect;
        private TimelineAspect _timelineAspect;
        
        private TimerAspect _timerAspect;

        // An ability in use (its timeline is still running) cannot be applied again.
        private ProtoItExc _filter = It
            .Chain<AbilityValidationSelfRequest>()
            .Inc<CooldownStateComponent>()
            .Inc<ActiveAbilityComponent>()
            .Exc<AbilityUsingComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                if (!_ownershipAspect.OwnerLink.Has(entity))
                    continue;

                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(entity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                    continue;

                if (!_timelineAspect.TryCreateTimelineEntity(entity, out var timelineEntity))
                    continue;

                ref var abilityCooldownComponent = ref _abilityAspect.CooldownState.Get(entity);
                abilityCooldownComponent.LastTime = GameTime.Time;

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Add(timelineEntity);

                // Targets are optional: a self ability has none.
                if (_abilityAspect.Targets.Has(ownerEntity))
                {
                    ref var targetsComponent = ref _abilityAspect.Targets.Get(ownerEntity);
                    for (var i = 0; i < targetsComponent.Count; i++)
                        abilityContextComponent.targets.Add(targetsComponent.Entities[i]);
                }

                abilityContextComponent.abilityEntity = entity.PackEntity(_world);
                abilityContextComponent.abilityOwner = ownerLinkComponent.Value;

                // Not ready until the cooldown timer finishes (AbilityCompleteCooldownSystem returns Active).
                // The timer starts on every use, so an ability without CooldownBehaviour gets cooldown 0, not a single use.
                _abilityAspect.Active.Del(entity);
                _timerAspect.Restart.GetOrAddComponent(entity);

                // In use until its timeline ends (CompleteAbilityOnTimelineEndSystem).
                _abilityAspect.AbilityUsing.GetOrAddComponent(entity);
                _abilityAspect.UsingEvent.GetOrAddComponent(entity);
            }
        }
    }
}