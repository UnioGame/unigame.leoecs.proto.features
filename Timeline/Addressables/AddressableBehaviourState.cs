namespace Game.Code.Timeline.Addressables
{
    using System;
    using UnityEngine;

    [Serializable]
    public class AddressableBehaviourState
    {
        [Range(0f,1f)]
        public float loadOnProgress = 0.5f;
        public bool singleLoadPerLifeTime = true;
        public bool ownLifeTime = true;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ReadOnly]
#endif
        public bool isLoaded = false;
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ReadOnly]
#endif
        public int counter;
    }
}