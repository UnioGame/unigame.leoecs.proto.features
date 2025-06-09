namespace Game.Code.Timeline.Editor
{
    using UnityEditor;

    using UnityEngine.Timeline;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
#endif
    
    [CustomEditor(typeof(TrackAsset))]
    public class OdinTrackDrawer 
#if ODIN_INSPECTOR
        : OdinEditor
#else 
        : Editor
#endif
    {
        
    }
}