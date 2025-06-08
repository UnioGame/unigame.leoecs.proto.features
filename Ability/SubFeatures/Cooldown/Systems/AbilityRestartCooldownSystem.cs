namespace UniGame.Ecs.Proto.Ability.SubFeatures.Cooldown.Systems
{
    using System;
    using Ability.Aspects;
    using Components;
    using FakeTimeline.Aspects;
    using FakeTimeline.Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Abstract;
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
    public class AbilityRestartCooldownSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private TimerAspect _timerAspect;
        private AbilityAspect _abilityAspect;
        private TimelineAspect _timelineAspect;

        private ProtoIt _playableFilter = It
            .Chain<CooldownRestartPlayableComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var playableEntity in _playableFilter)
            {
                ref var executeComponent = ref _timelineAspect.TimelineExecute.Get(playableEntity);
                if (!executeComponent.TimelineContextEntity.Unpack(_world, out var contextEntity))
                {
                    continue;
                }

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Get(contextEntity);
                if (!abilityContextComponent.abilityEntity.Unpack(_world, out var abilityEntity))
                {
                    continue;
                }
                
                _timerAspect.Restart.GetOrAddComponent(abilityEntity);
            }
        }
    }
}