namespace Game.Ecs.Input.Data.ActionMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [Serializable]
    [ValueDropdown("@Game.Ecs.Input.Data.InputActionId.GetIds()")]
    public struct InputActionId : IEquatable<int>
    {
        [SerializeField]
        public int value;

        #region statis editor data

        private static InputActionsMapData _inputActionsData;

        public static IEnumerable<ValueDropdownItem<InputActionId>> GetIds()
        {
#if UNITY_EDITOR
            _inputActionsData ??= AssetEditorTools.GetAsset<InputActionsMapData>();
            var inputActionData = _inputActionsData;
            if (inputActionData == null)
            {
                yield return new ValueDropdownItem<InputActionId>()
                {
                    Text = "EMPTY",
                    Value = (InputActionId)0,
                };
                yield break;
            }

            foreach (var inputActionMap in inputActionData.inputActionsMaps)
            {
                yield return new ValueDropdownItem<InputActionId>()
                {
                    Text = inputActionMap.name,
                    Value = (InputActionId)inputActionMap.id,
                };
            }
#endif
            yield break;
        }

        public static string GetName(InputActionId inputActionsMapId)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(InputActionId v)
        {
            return v.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator InputActionId(int v)
        {
            return new InputActionId { value = v };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => value.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InputActionId FromInt(int data)
        {
            return (InputActionId)data;
        }

        public bool Equals(int other)
        {
            return value == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is InputActionId mask)
                return mask.value == value;

            return false;
        }
    }
}