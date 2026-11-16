namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
    using System;

    /// <summary>
    /// Search ability in inventory to champion
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EquipAbilityIdToChampionRequest
    {
        public int AbilityId;
        public int AbilitySlot;
        public bool IsUserInput;
        public bool IsDefault;
    }
}