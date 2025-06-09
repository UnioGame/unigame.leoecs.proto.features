namespace Game.Code.Services.AbilityLoadout.Data
{
    using System;
    using System.Collections.Generic;
    using UniGame.Core.Runtime;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AbilityData
    {
#if ODIN_INSPECTOR
        [ValueDropdown(nameof(GetAbilitiesSlots))]
#endif
        public int slotType;

        public bool isDefault;
        public bool isHidden;
        public bool isBlock;
		
        private IEnumerable<ValueDropdownItem<int>> GetAbilitiesSlots()
        {
            foreach (var slotId in AbilitySlotId.GetAbilitySlotIds())
                yield return slotId;
        }
    }
}