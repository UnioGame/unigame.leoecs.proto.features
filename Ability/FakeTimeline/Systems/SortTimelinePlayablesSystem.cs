namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Unity.Collections;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SortTimelinePlayablesSystem : IProtoInitSystem, IProtoRunSystem
    {
        private TimelineBehaviourComparer _comparer;

        private TimelineAspect _timelineAspect;

        private ProtoItExc _timelinePrototypeFilter = It
            .Chain<TimelinePrototypeComponent>()
            .Exc<TimelineReadyComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _comparer = new TimelineBehaviourComparer(_timelineAspect);
        }
        
        public void Run()
        {
            foreach (var timelineEntity in _timelinePrototypeFilter)
            {
                ref var timelineComponent = ref _timelineAspect.TimelinePrototype.Get(timelineEntity);
                timelineComponent.playables.Sort(_comparer);

                _timelineAspect.TimelineReady.Add(timelineEntity);
            }
        }
    }
}