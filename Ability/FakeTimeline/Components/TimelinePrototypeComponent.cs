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
    public struct TimelinePrototypeComponent : IProtoAutoReset<TimelinePrototypeComponent>
    {
        public NativeList<ProtoPackedEntity> playables;
        
        public void AutoReset(ref TimelinePrototypeComponent c)
        {
            if (c.playables.IsCreated)
            {
                c.playables.Clear();
            }
            else
            {
                c.playables = new NativeList<ProtoPackedEntity>(12, Allocator.Persistent);
            }
        }

        public void Add(ProtoPackedEntity packedEntity)
        {
            playables.Add(packedEntity);
        }
    }
}