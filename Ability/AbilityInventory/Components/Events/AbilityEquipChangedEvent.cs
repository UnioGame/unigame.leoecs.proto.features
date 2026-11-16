namespace UniGame.Ecs.Proto.AbilityInventory.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Notify about ability equip changed
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityEquipChangedEvent
    {
        public int AbilityId;
        public int AbilitySlot;
        public ProtoPackedEntity Owner;
        public ProtoPackedEntity AbilityEntity;
    }
}