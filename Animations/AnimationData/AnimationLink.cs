namespace Game.Code.Animations
{
    using EffectMilestones;

    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [CreateAssetMenu(fileName = "Animation Link", menuName = "Game/Animation/Animation Link")]
    public sealed class AnimationLink : ScriptableObject
    {
#if ODIN_INSPECTOR
        [OnValueChanged(nameof(BakeMilestones))]
#endif
        [FormerlySerializedAs("_animation")] 
        [SerializeField]
        public PlayableAsset animation;

        public DirectorWrapMode wrapMode = DirectorWrapMode.Hold;
        
        [Tooltip("If animationSpeed is 0, animation will be played with default calculated speed")]
        public float animationSpeed = 0f;
        
        [Tooltip("If duration is 0, animation will be played with default playable asset duration")]
        public float duration = 0;
        
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public PlayableBindingData bindingData = new PlayableBindingData();

#if ODIN_INSPECTOR
        [PropertySpace(8)]
        [InlineProperty]
        [HideLabel]
#endif
        [Space]
        [SerializeField]
        public EffectMilestonesData milestones;

        public bool showCommands = true;
        
        public float Duration => duration <= 0 && animation!=null ? (float)animation.duration : duration ;
        
#if ODIN_INSPECTOR
        [ButtonGroup()]
        [ShowIf(nameof(showCommands))]
#endif
        public void BakeMilestones()
        {
            AnimationTool.BakeMilestones(milestones,animation);
#if UNITY_EDITOR
            this.MarkDirty();
#endif
        }
        
#if ODIN_INSPECTOR
        [ButtonGroup]
        [ShowIf(nameof(showCommands))]
#endif
        public void Clear()
        {
            bindingData.Clear();
            milestones.Clear();
#if UNITY_EDITOR
            this.MarkDirty();
#endif
        }

#if ODIN_INSPECTOR
        [Button(SdfIconType.DoorOpen)]
        [ShowIf(nameof(showCommands))]
#endif
        public void OpenEditor()
        {
            AnimationEditorData.OpenEditor(this);
        }

    }
}