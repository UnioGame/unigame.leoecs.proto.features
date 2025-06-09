namespace UniGame.Ecs.Proto.Gameplay.Dodge.Aspects
{
    using System;
    using Events;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Represents the aspect for the Dodge game mechanic.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DodgeAspect : EcsAspect
    {
        public ProtoPool<MissedEvent> Missed;
    }
}