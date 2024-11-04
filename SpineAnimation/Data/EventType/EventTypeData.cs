namespace Game.Ecs.SpineAnimation.Data.EventType
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public class EventTypeData
    {
        [InlineProperty]
        public List<EventType> collection = new();
    }
}