namespace UniGame.Ecs.Proto.Effects.Data
{
    using System;
    using System.Collections.Generic;
    using Core.Runtime;


#if UNITY_EDITOR
    using UniModules.Editor;
#endif
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [Serializable]
#if ODIN_INSPECTOR
    [ValueDropdown("@unigame.ecs.proto.Effects.Data.EffectRootId.EffectsRoots()",IsUniqueList = true,DropdownTitle = "Equip Slot")]
#endif
    public struct EffectRootId
    {
        public int value;

        #region statis editor data

#if UNITY_EDITOR
        private static EffectsRootConfiguration equipSlotsAsset;
        public static EffectsRootConfiguration EquipSlotsAsset => 
            equipSlotsAsset ??= AssetEditorTools.GetAsset<EffectsRootConfiguration>();
#endif
        
        public static IEnumerable<ValueDropdownItem<EffectRootId>> EffectsRoots()
        {
#if UNITY_EDITOR
            var data = EquipSlotsAsset?.data;
            if (data == null)
            {
                yield return new ValueDropdownItem<EffectRootId>()
                {
                    Text = "EMPTY",
                    Value = (EffectRootId)0,
                };
                yield break;
            }

            for (var i = 0; i < data.roots.Length; i++)
            {
                var key = data.roots[i];
                yield return new ValueDropdownItem<EffectRootId>()
                {
                    Text = key.name,
                    Value = (EffectRootId)i,
                };
            }

#endif
            yield break;
        }
                
        #endregion

        public static implicit operator int(EffectRootId v)
        {
            return v.value;
        }

        public static explicit operator EffectRootId(int v)
        {
            return new EffectRootId { value = v };
        }

        public override string ToString() => value.ToString();

        public override int GetHashCode() => value;

        public EffectRootId FromInt(int data)
        {
            value = data;
            return this;
        }
        
        public EffectRootId FromInt(string key)
        {
            value = key.GetHashCode();
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is EffectRootId mask)
                return mask.value == value;
            
            return false;
        }

    }
}