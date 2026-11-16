namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Systems
{
    using System;
    using System.Runtime.CompilerServices;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Time.Service;
    using UniGame.Proto.Ownership;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Проигрывает таймлайн способности: когда приходит время шага, просит его выполнить. Последний шаг
    /// сыгран — кидает TimelineEndEvent и удаляет таймлайн.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class TimelineSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private TimelineAspect _timelineAspect;
        private OwnershipAspect _ownershipAspect;

        private ProtoItExc _timelineFilter = It
            .Chain<TimelineComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var timelineEntity in _timelineFilter)
            {
                var timelinePackedEntity = timelineEntity.PackEntity(_world);
                
                ref var timelineComponent = ref _timelineAspect.Timeline.Get(timelineEntity);
                var playables = timelineComponent.playables;
                var index = timelineComponent.index;

                if (!playables.IsCreated || index >= playables.Length)
                {
                    EndTimeline(timelineEntity, timelinePackedEntity);
                    continue;
                }
                
                var timelineDelta = GameTime.Time - timelineComponent.playStartTime;
                var currBehaviourEntity = playables[index];
                while (IsPlayableReady(timelinePackedEntity, currBehaviourEntity, timelineDelta))
                {
                    index++;
                    if (index >= playables.Length)
                    {
                        EndTimeline(timelineEntity, timelinePackedEntity);
                        break;
                    }
                    
                    currBehaviourEntity = playables[index];
                    timelineComponent.index = index;
                }
            }
        }

        // TimelineEndEvent lets the owner of the timeline (an ability) react to its end in the same frame.
        private void EndTimeline(ProtoEntity timelineEntity, ProtoPackedEntity timelinePackedEntity)
        {
            ref var endEvent = ref _timelineAspect.TimelineEndedEvent.GetOrAddComponent(timelineEntity);
            endEvent.ContextPackedEntity = timelinePackedEntity;
            _ownershipAspect.Kill(timelineEntity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsPlayableReady(ProtoPackedEntity timelineEntity, ProtoPackedEntity packedPlayableEntity, 
            float timelineDelta)
        {
            if (!packedPlayableEntity.Unpack(_world, out var playableEntity))
            {
                return true;
            }

            ref var timelinePlayableComponent = ref _timelineAspect.TimelinePlayable.Get(playableEntity);
            if (timelinePlayableComponent.delay <= timelineDelta)
            {
                ref var executePlayableComponent = ref _timelineAspect.TimelineExecute.GetOrAdd(playableEntity);
                executePlayableComponent.TimelineContextEntity = timelineEntity;
                return true;
            }

            return false;
        }
    }
}