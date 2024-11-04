namespace Game.Ecs.SpineAnimation.Components
{
    using System;
    using System.Collections.Generic;
    using Data.EventType;
    using Spine.Unity;

    /// <summary>
    /// Component that holds a dictionary of Spine events.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SpineAnimationEventsComponent
    {
        public Dictionary<EventTypeId, EventDataReferenceAsset> Events;
    }
}