namespace Game.Code.Animations.EffectMilestones.Timeline
{
    using System.ComponentModel;
    using UniCore.Runtime.ProfilerTools;
    using UnityEngine.Timeline;

    [TrackClipType(typeof(EffectMilestoneClip))]
    [DisplayName("Ability/Effects Milestones Track")]
    public sealed class EffectMilestonesTrack : TrackAsset
    {
        protected override void OnCreateClip(TimelineClip clip)
        {
            GameLog.Log("Hello yoba!");
        }
    }
}