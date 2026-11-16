namespace UniGame.Ecs.Proto.Ability.Common.Components
{
    using System;
    using Game.Code.Configuration.Runtime.Ability;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Запрос выдать способность прямо из конфига (AbilityConfiguration), без инвентаря и каталога.
    /// Сущность запроса сама становится способностью.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EquipAbilityConfigurationSelfRequest
    {
        public ProtoPackedEntity Owner;
        public AbilityConfiguration Configuration;
        public int Slot;
        public bool IsDefault;
    }
}