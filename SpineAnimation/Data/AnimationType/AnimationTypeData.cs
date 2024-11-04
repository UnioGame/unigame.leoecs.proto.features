namespace Game.Ecs.SpineAnimation.Data.AnimationType
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public class AnimationTypeData
    {
        [InlineProperty]
        public List<AnimationType> collection = new();
    }
}