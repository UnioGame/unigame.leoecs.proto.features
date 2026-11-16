namespace UniGame.Ecs.Proto.Characteristics.AttackSpeed.Components
{
    using System;
    using Cooldown;

    /// <summary>
    /// Как трактовать скорость атаки: как кулдаун в секундах или как количество ударов в секунду.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackSpeedCooldownTypeComponent
    {
        public CooldownType Value;
    }
}