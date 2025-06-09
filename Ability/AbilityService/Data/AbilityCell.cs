namespace Game.Code.Configuration.Runtime.Ability
{
    using System;
    using Description;
    using Services.AbilityLoadout.Data;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    [Serializable]
    public struct AbilityCell
    {
        public bool IsDefault;
        
#if ODIN_INSPECTOR
        [InlineButton(nameof(PingAbility), "Ping")]
#endif
        public AbilityId AbilityId;
        public AbilitySlotId SlotId;
        
        public AbilityCell(AbilityId abilityId, AbilitySlotId slotId, bool isDefault = false)
        {
            AbilityId = abilityId;
            SlotId = slotId;
            IsDefault = isDefault;
        }


        private void PingAbility()
        {
#if UNITY_EDITOR
            var record = AbilityId.GetAbilityRecord(AbilityId);
            if (record == null) return;
            var asset = record.ability.EditorValue;
            if (asset == null) return;
            asset.PingInEditor();
#endif
        }
     
    }
}