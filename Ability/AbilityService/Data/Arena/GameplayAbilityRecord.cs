namespace Game.Code.Services.Ability.Data.Arena
{
    using System;


#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class GameplayAbilityRecord
    {
        public AbilityCategoryId categoryId;
        public bool isPassive = false;
        
#if ODIN_INSPECTOR
        [HideLabel]
        [InfoBox("Ability assets by level", InfoMessageType.None)]
        [ShowInInspector]
        [TableList]
#endif
        public AbilityRef[] abilityAssets = Array.Empty<AbilityRef>();
    }
}