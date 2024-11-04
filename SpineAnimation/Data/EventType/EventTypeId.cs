namespace Game.Ecs.SpineAnimation.Data.EventType
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [Serializable]
    [ValueDropdown("@Game.Ecs.SpineAnimation.Data.EventType.EventTypeId.GetEventTypes()", IsUniqueList = true, DropdownTitle = "EventType")]
    public partial struct EventTypeId
    {
        [SerializeField]
        public int value;

        #region static editor data

        private static EventTypeMap _map;

        public static IEnumerable<ValueDropdownItem<EventTypeId>> GetEventTypes()
        {
#if UNITY_EDITOR
            _map ??= AssetEditorTools.GetAsset<EventTypeMap>();
            var types = _map;
            if (types == null)
            {
                yield return new ValueDropdownItem<EventTypeId>()
                {
                    Text = "EMPTY",
                    Value = (EventTypeId)0,
                };
                yield break;
            }

            foreach (var type in types.value.collection)
            {
                yield return new ValueDropdownItem<EventTypeId>()
                {
                    Text = type.name,
                    Value = (EventTypeId)type.id,
                };
            }
#endif
            yield break;
        }

        public static string GetEventTypeName(EventTypeId slotId)
        {
#if UNITY_EDITOR
            var types = GetEventTypes();
            var filteredTypes = types
                .FirstOrDefault(x => x.Value == slotId);
            var slotName = filteredTypes.Text;
            return string.IsNullOrEmpty(slotName) ? string.Empty : slotName;
#endif
            return string.Empty;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            _map = null;
        }

        #endregion

        public static implicit operator int(EventTypeId v)
        {
            return v.value;
        }

        public static explicit operator EventTypeId(int v)
        {
            return new EventTypeId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public EventTypeId FromInt(int data)
        {
            value = data;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is EventTypeId mask)
                return mask.value == value;

            return false;
        }
    }
}