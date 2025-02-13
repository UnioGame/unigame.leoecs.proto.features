namespace Game.Ecs.State.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UniModules.Editor;
    using UnityEngine;

    [Serializable]
    [ValueDropdown("@Game.Ecs.State.Data.StateId.GetStates()", IsUniqueList = true, DropdownTitle = "State")]
    public struct StateId
    {
        public static StateId Empty = new StateId { value = 0 };
        
        [SerializeField]
        public int value;

        #region static editor data

        private static StateDataAsset _dataAsset;

        public static IEnumerable<ValueDropdownItem<StateId>> GetStates()
        {
#if UNITY_EDITOR
            _dataAsset ??= AssetEditorTools.GetAsset<StateDataAsset>();
            var types = _dataAsset;
            if (types == null)
            {
                yield return new ValueDropdownItem<StateId>()
                {
                    Text = "EMPTY",
                    Value = Empty,
                };
                yield break;
            }

            foreach (var type in types.Types)
            {
                yield return new ValueDropdownItem<StateId>()
                {
                    Text = type.Name,
                    Value = (StateId)type.Id,
                };
            }
#endif
            yield break;
        }

        public static string GetStateName(StateId slotId)
        {
#if UNITY_EDITOR
            var types = GetStates();
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
            _dataAsset = null;
        }

        #endregion

        public static implicit operator int(StateId v)
        {
            return v.value;
        }

        public static explicit operator StateId(int v)
        {
            return new StateId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public StateId FromInt(int data)
        {
            value = data;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is StateId mask)
                return mask.value == value;

            return false;
        }
    }
}