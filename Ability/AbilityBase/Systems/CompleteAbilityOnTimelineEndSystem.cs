namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.FakeTimeline.Components.Events;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Proto.Ability;

    /// <summary>
    /// When an ability timeline ends, asks to complete its ability (CompleteAbilitySelfRequest);
    /// CompleteAbilitySystem then removes AbilityUsingComponent and fires AbilityCompleteSelfEvent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CompleteAbilityOnTimelineEndSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;

        private ProtoIt _timelineEndFilter = It
            .Chain<TimelineEndEvent>()
            .Inc<AbilityContextComponent>()
            .End();

        public void Run()
        {
            foreach (var timelineEntity in _timelineEndFilter)
            {
                ref var context = ref _abilityAspect.AbilityContext.Get(timelineEntity);
                if (!context.abilityEntity.Unpack(_world, out var abilityEntity))
                    continue;

                if (!_abilityAspect.AbilityUsing.Has(abilityEntity))
                    continue;

                _abilityAspect.CompleteAbility.GetOrAddComponent(abilityEntity);
            }
        }
    }
}