namespace Game.Code.Animations
{

    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [CreateAssetMenu(fileName = "Animation Binding Data", menuName = "Game/Animation/Animation Binding Data")]
    public sealed class AnimationBindingData : ScriptableObject
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        public PlayableBindingData data = new PlayableBindingData();
    }
    
}