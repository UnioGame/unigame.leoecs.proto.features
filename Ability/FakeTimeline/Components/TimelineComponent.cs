namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Components
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Unity.Collections;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct TimelineComponent : IProtoAutoReset<TimelineComponent>
    {
        public NativeList<ProtoPackedEntity> playables;
        public int index;

        public float playStartTime;
        
        public void AutoReset(ref TimelineComponent c)
        {
            if (!c.playables.IsCreated)
            {
                c.playables = new NativeList<ProtoPackedEntity>(16, Allocator.Persistent);
            }
            else
            {
                c.playables.Clear();
            }

            c.index = 0;
        }
    }
}