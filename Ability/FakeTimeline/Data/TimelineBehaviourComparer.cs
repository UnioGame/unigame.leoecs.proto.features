namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Data
{
    using System.Collections.Generic;
    using Aspects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto.QoL;

    public class TimelineBehaviourComparer : IComparer<ProtoPackedEntity>
    {
        private readonly TimelineAspect _aspect;

        public TimelineBehaviourComparer(TimelineAspect aspect)
        {
            _aspect = aspect;
        }
        
        public int Compare(ProtoPackedEntity x, ProtoPackedEntity y)
        {
            if (!(x.Unpack(_aspect.world, out var ux) && y.Unpack(_aspect.world, out var uy)))
            {
                return 0;
            }

            ref var xComponent = ref _aspect.TimelinePlayable.Get(ux);
            ref var yComponent = ref _aspect.TimelinePlayable.Get(uy);

            var xValue = xComponent.delay;
            var yValue = yComponent.delay;

            if (xValue < yValue)
            {
                return -1;
            }

            if (xValue > yValue)
            {
                return 1;
            }

            return 0;
        }
    }
}