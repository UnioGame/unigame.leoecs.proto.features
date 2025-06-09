namespace Game.Modules.Feature.SequenceActions
{
    using Unity.IL2CPP.CompilerServices;
    using System;
    using System.Collections.Generic;
    using Data;
    using UniGame.Core.Runtime;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
#if ODIN_INSPECTOR
    [ValueDropdown("@Game.Modules.Feature.SequenceActions.SequenceId.GetIds()")]
#endif
    public struct SequenceId
    {
        [SerializeField]
        public string id;

        public string Id => id;

        public static implicit operator string(SequenceId abilityId) => abilityId.Id;

        public static explicit operator SequenceId(string abilityId) => new SequenceId { id = abilityId };

        public override string ToString() => id;

        public override int GetHashCode() => string.IsNullOrEmpty(id)
            ? 0
            : id.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is SequenceId abilityId)
                return abilityId.Id == Id;

            return false;
        }

        public static IEnumerable<ValueDropdownItem<SequenceId>> GetIds()
        {
#if UNITY_EDITOR

            var configuration = AssetEditorTools.GetAsset<SequenceActionsMapAsset>();
            foreach (var id in configuration.actions)
            {
                var actionName = id.ActionName;
                yield return new
                    ValueDropdownItem<SequenceId>()
                    {
                        Text = actionName,
                        Value = (SequenceId)actionName
                    };
            }

#endif

            yield break;
        }

    }
}