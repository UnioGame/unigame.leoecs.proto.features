namespace Game.Code.Timeline.Editor
{
    using Addressables;
    using UnityEditor;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
#endif
    
    [CustomEditor(typeof(AddressableLoadGameObjectAnimationTrack))]
    public class OdinAddressableGameObjectTrackDrawer 
#if ODIN_INSPECTOR
        : OdinEditor
#else 
        : Editor
#endif
    {
        
    }
}