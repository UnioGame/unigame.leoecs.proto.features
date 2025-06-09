namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public struct EffectRootValue
    {
        public EffectRootId id;
        
#if ODIN_INSPECTOR
        [OnValueChanged(nameof(OnObjectChanged))]
#endif
        public GameObject objectValue;
        public string objectName;

        private void OnObjectChanged(GameObject gameObject)
        {
            objectName = gameObject== null ? string.Empty : gameObject.name;
        }
    }
}