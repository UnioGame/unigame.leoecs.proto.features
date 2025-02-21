namespace Game.Ecs.State.Systems
{
    using System;
    using Leopotam.EcsProto;

    [Serializable]
    public abstract class StateSystem : IProtoSystem
    {
        public virtual string Name => GetType().Name;
    }
}