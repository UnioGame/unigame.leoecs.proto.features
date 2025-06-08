namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Game.Ecs.Time.Service;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class TimelineAspect : EcsAspect
    {
        private OwnershipAspect _ownershipAspect;
        
        public ProtoPool<TimelineComponent> Timeline;
        public ProtoPool<TimelinePlayableComponent> TimelinePlayable;
        public ProtoPool<TimelineReadyComponent> TimelineReady;

        public ProtoPool<TimelinePrototypeComponent> TimelinePrototype;
        
        // Requests
        public ProtoPool<ExecuteTimelinePlayableRequest> TimelineExecute;
        
        // Events
        public ProtoPool<TimelineEndEvent> TimelineEndedEvent;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity CreateTimelineEntity(ProtoEntity timelinePrototypeEntity)
        {
            if (!TimelinePrototype.Has(timelinePrototypeEntity))
            {
#if UNITY_EDITOR || DEBUG
                Debug.LogError("TimelineAspect: Entity does not contain TimelinePrototype component.");
#endif
                return default;
            }

            ref var timelinePrototypeComponent = ref TimelinePrototype.Get(timelinePrototypeEntity);
            
            var timelineEntity = world.NewEntity();
            ref var timelineComponent = ref Timeline.Add(timelineEntity);
            timelineComponent.playables.CopyFrom(timelinePrototypeComponent.playables);
            timelineComponent.playStartTime = GameTime.Time;
            
            _ownershipAspect.AddChild(timelinePrototypeEntity, timelineEntity);

            return timelineEntity;
        }
    }
}