namespace UniGame.Ecs.Proto.Ability.Components.Requests
{
    using System;

    /// <summary>
    /// Запрос поменять базовый кулдаун способности в слоте.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SetAbilityBaseCooldownSelfRequest
    {
        public float Cooldown;
        public int AbilitySlot;
    }
}