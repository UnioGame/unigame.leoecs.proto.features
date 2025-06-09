namespace Game.Ecs.Scenes.Data
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using UniGame.Core.Runtime;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if ODIN_INSPECTOR
    [ValueDropdown("@Game.Ecs.Scenes.Data.SceneId.GetSceneInfoIds()", IsUniqueList = true, DropdownTitle = "SceneId")]
#endif
    [Serializable]
    public partial struct SceneId
    {
        [SerializeField]
        public int value;

        #region static editor data

        private static SceneMap _dataAsset;

        public static IEnumerable<ValueDropdownItem<SceneId>> GetSceneInfoIds()
        {
#if UNITY_EDITOR
            _dataAsset ??= AssetEditorTools.GetAsset<SceneMap>();
            var types = _dataAsset;
            if (types == null)
            {
                yield return new ValueDropdownItem<SceneId>()
                {
                    Text = "EMPTY",
                    Value = (SceneId)0,
                };
                yield break;
            }

            foreach (var type in types.collection)
            {
                yield return new ValueDropdownItem<SceneId>()
                {
                    Text = type.Name,
                    Value = (SceneId)type.Value,
                };
            }
#endif
            yield break;
        }

        public static string GetSceneInfoIdName(SceneId slotId)
        {
#if UNITY_EDITOR
            var types = GetSceneInfoIds();
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

        public static implicit operator int(SceneId v)
        {
            return v.value;
        }

        public static explicit operator SceneId(int v)
        {
            return new SceneId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public SceneId FromInt(int data)
        {
            value = data;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is SceneId mask)
                return mask.value == value;

            return false;
        }
    }
}