namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Data
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
    [ValueDropdown("@Game.Ecs.ButtonAction.SubFeatures.MainAction.Data.MainActionId.GetMainActions()", IsUniqueList = true, DropdownTitle = "MainAction")]
    public partial struct GameActionId : IEquatable<int>
    {
        public static GameActionId Empty => new GameActionId { value = -1 };
        
        [SerializeField]
        public int value;

        #region static editor data

        private static GameActionsMap _map;

        public static IEnumerable<ValueDropdownItem<GameActionId>> GetMainActions()
        {
#if UNITY_EDITOR
            _map ??= AssetEditorTools.GetAsset<GameActionsMap>();
            var types = _map;
            if (types == null)
            {
                yield return new ValueDropdownItem<GameActionId>()
                {
                    Text = nameof(Empty),
                    Value = Empty,
                };
                yield break;
            }

            foreach (var type in types.value.collection)
            {
                yield return new ValueDropdownItem<GameActionId>()
                {
                    Text = type.name,
                    Value = (GameActionId)type.id,
                };
            }
#endif
            yield break;
        }

        public static string GetMainActionName(GameActionId slotId)
        {
#if UNITY_EDITOR
            var types = GetMainActions();
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

        public static implicit operator int(GameActionId v)
        {
            return v.value;
        }

        public static explicit operator GameActionId(int v)
        {
            return new GameActionId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public GameActionId FromInt(int data)
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
            if (obj is GameActionId mask)
                return mask.value == value;

            return false;
        }
    }
}