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
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Timer.Components;

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
        
        private ProtoIt _filter = It
            .Chain<AbilityValidationSelfRequest>()
            .Inc<CooldownStateComponent>()
            .Inc<ActiveAbilityComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var abilityCooldownComponent = ref _abilityAspect.CooldownState.Get(entity);
                abilityCooldownComponent.LastTime = GameTime.Time;
                
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(entity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                {
                    continue;
                }
                
                ref var targetsComponent = ref _abilityAspect.Targets.Get(ownerEntity);
                var timelineEntity = _timelineAspect.CreateTimelineEntity(entity);
                
                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Add(timelineEntity);
                for (int i = 0; i < targetsComponent.Count; i++)
                {
                    abilityContextComponent.targets.Add(targetsComponent.Entities[i]);
                }

                abilityContextComponent.abilityEntity = entity.PackEntity(_world);
                abilityContextComponent.abilityOwner = ownerLinkComponent.Value;

                _abilityAspect.Active.Del(entity);
            }
        }
    }
}