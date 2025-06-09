namespace UniGame.Ecs.Proto.Tools.Converters
{
    using Game.Code.Animations;

    using UnityEngine;
    using UnityEngine.Playables;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
#endif
    
    public class TimeLineAnimationBaker : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [PropertySpace(8)]
        [InlineEditor]
#endif
        public AnimationLink animationLink;

#if ODIN_INSPECTOR
        [PropertySpace(8)]
#endif

        public PlayableDirector director;

        public bool IsDataAvailable => director !=null && animationLink != null && animationLink.animation != null;
        
#if ODIN_INSPECTOR
        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
#endif
        public void Bake()
        {
            AnimationTool.BakeAnimationLink(director, animationLink);
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
#endif
        public void Apply()
        {
            AnimationTool.ApplyBindings(director, animationLink);
        }

#if ODIN_INSPECTOR
        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
#endif
        public void ClearTimeline()
        {
            AnimationTool.ClearReferences(director, animationLink.animation);
        }
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ClearBacking()
        {
            animationLink?.Clear();
        }
        
        
    }
}