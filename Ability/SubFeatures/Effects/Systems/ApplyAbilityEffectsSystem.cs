namespace Game.Modules.leoecs.proto.features.Ability.SubFeatures.Effects.Systems
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Ability.Aspects;
    using UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Aspects;
    using UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Components.Requests;
    using UniGame.Ecs.Proto.Effects;
    using UniGame.Ecs.Proto.Effects.Aspects;
    using UniGame.Ecs.Proto.Effects.Components;
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
    public class ApplyAbilityEffectsSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private AbilityAspect _abilityAspect;
        private EffectAspect _effectAspect;
        private TimelineAspect _timelineAspect;

        private ProtoIt _applyEffectsFilter = It
            .Chain<EffectsComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var applyEffectsRequest in _applyEffectsFilter)
            {
                ref var executeComponent = ref _timelineAspect.TimelineExecute.Get(applyEffectsRequest);
                if (!executeComponent.TimelineContextEntity.Unpack(_world, out var contextEntity))
                {
                    continue;
                }

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Get(contextEntity);
                var target = abilityContextComponent.FirstTarget();
                var source = _world.PackEntity(applyEffectsRequest);
                
                ref var effectsComponent = ref _effectAspect.EffectsComponent.Get(applyEffectsRequest);
                effectsComponent.Effects.CreateRequests(_world, source, target);
            }
        }
    }
}