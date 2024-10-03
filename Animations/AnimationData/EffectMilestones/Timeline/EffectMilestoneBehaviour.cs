namespace Game.Code.Animations.EffectMilestones.Timeline
{
    using UniCore.Runtime.ProfilerTools;
    using UnityEngine.Playables;

    public sealed class EffectMilestoneBehaviour : PlayableBehaviour
    {
        public override void OnGraphStart(Playable playable)
        {
            GameLog.LogWarning("Timeline started!");
        }
        
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            GameLog.LogWarning("Behaviour playing!");
        }
    }
}