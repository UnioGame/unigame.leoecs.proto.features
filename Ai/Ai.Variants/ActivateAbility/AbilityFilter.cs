namespace Game.Code.Ai.ActivateAbility
{
    using System;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif 
    
    [Serializable]
    public struct AbilityFilter
    {
        public bool UsePriority;
        
        /// <summary>
        /// Приоритет выбора. Первые элементы более приоритетны остальным
        /// </summary>
#if ODIN_INSPECTOR
        [ShowIf(nameof(UsePriority))]
#endif 
        public CategoryPriority[] Priorities;

        /// <summary>
        /// ability slot number
        /// </summary>
        public int Slot;
    }
}