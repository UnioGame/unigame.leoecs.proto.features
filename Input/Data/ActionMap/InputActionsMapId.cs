namespace Game.Ecs.Input.Data.ActionMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniGame.Core.Runtime;
    using UnityEngine;
    using UnityEngine.Serialization;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif

#if ODIN_INSPECTOR
    [ValueDropdown("@Game.Ecs.Input.Data.InputActionsMapId.GetIds()")]
#endif
    [Serializable]
    public struct InputActionsMapId : IEquatable<int>
    {
        [SerializeField]
        public int value;

        #region statis editor data

        private static InputActionsMapData _inputActionsData;
        
        public static IEnumerable<ValueDropdownItem<InputActionsMapId>> GetIds()
        {
#if UNITY_EDITOR
            _inputActionsData ??= AssetEditorTools.GetAsset<InputActionsMapData>();
            var inputActionData = _inputActionsData;
            if (inputActionData == null)
            {
                yield return new ValueDropdownItem<InputActionsMapId>()
                {
                    Text = "EMPTY",
                    Value = (InputActionsMapId)0,
                };
                yield break;
            }

            foreach (var inputActionMap in inputActionData.inputActionsMaps)
            {
                yield return new ValueDropdownItem<InputActionsMapId>()
                {
                    Text = inputActionMap.name,
                    Value = (InputActionsMapId)inputActionMap.id,
                };
            }
#endif
            yield break;
        }

        public static string GetName(InputActionsMapId inputActionsMapId)
        {
#if UNITY_EDITOR
            var ids = GetIds();
            var item = ids.FirstOrDefault(x => x.Value == inputActionsMapId);
            var itemText = item.Text;
            return string.IsNullOrEmpty(itemText) ? string.Empty : itemText;
#endif
            return string.Empty;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            _inputActionsData = null;
        }

        #endregion

        public static implicit operator int(InputActionsMapId v)
        {
            return v.value;
        }

        public static explicit operator InputActionsMapId(int v)
        {
            return new InputActionsMapId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public InputActionsMapId FromInt(int data)
        {
            value = data;

            return this;
        }

        public bool Equals(int other)
        {
            return value == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is InputActionsMapId mask)
                return mask.value == value;

            return false;
        }
    }
}