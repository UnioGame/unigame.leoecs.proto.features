namespace Game.Code.Animations.EffectMilestones
{
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(fileName = "Effect Milestones Info", menuName = "Game/Configurations/Ability/Effect Milestones Info")]
    public sealed class EffectMilestonesInfo : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public EffectMilestonesData data = new EffectMilestonesData();

        public void AddMilestone(float time) => data.AddMilestone(time);

        public void Clear() => data.Clear();
    }
}