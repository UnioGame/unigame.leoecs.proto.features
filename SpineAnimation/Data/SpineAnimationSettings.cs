namespace Game.Ecs.SpineAnimation.Data
{
    using System;
    using Spine.Unity;

    [Serializable]
    public class SpineAnimationSettings
    {
        public AnimationReferenceAsset animation;
        public int trackIndex;
        public bool loop;
    }
}