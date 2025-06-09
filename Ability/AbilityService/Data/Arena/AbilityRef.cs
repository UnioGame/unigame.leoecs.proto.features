namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using Game.Code.Services.AbilityLoadout.Data;

    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class AbilityRef
    {
#if ODIN_INSPECTOR
        [TableColumnWidth(50, resizable: false)]
#endif
        public int level;
        
#if ODIN_INSPECTOR
        [TableColumnWidth(150, resizable: true)]
#endif
        public AssetReferenceAbility ability;
        [HideInInspector]
        public int id;
    }
}