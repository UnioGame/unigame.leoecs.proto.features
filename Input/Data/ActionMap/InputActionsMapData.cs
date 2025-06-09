namespace Game.Ecs.Input.Data.ActionMap
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif
    
    [CreateAssetMenu(menuName = "Game/Maps/Input ActionMaps Map", fileName = "Input ActionMaps Map")]
    public class InputActionsMapData : ScriptableObject
    {
        [HideInInspector]
        public InputActionsMap defaultInputActionsMap = new InputActionsMap();
        
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public InputActionsMap[] inputActionsMaps = Array.Empty<InputActionsMap>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InputActionsMap GetInputActionMap(int id)
        {
            if (id < 0 || id >= inputActionsMaps.Length)
                return defaultInputActionsMap;

            return inputActionsMaps[id];
        }
    }
}