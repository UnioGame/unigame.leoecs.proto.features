namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Components
{
    using System;

    /// <summary>
    /// Эффект «урон от атаки»: бьёт на величину AttackDamage того, кто применил эффект.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AttackDamageEffectComponent
    {
    }
}