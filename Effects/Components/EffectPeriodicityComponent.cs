namespace UniGame.Ecs.Proto.Effects.Components
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;

    /// <summary>
    /// Как часто эффект срабатывает: -1 — один раз, иначе каждые N секунд.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct EffectPeriodicityComponent : IProtoAutoReset<EffectPeriodicityComponent>
    {
        /// <summary>
        /// Периодичность эффекта.
        /// </summary>
        public float Periodicity;
        
        /// <summary>
        /// Последнее время применения эффекта. Если <see cref="LastApplyingTime"/> + <see cref="Periodicity"/> больше
        /// или равен текущему игровому времени, то эффект применится.
        /// </summary>
        public float LastApplyingTime;
        
        public void SetHandlers(IProtoPool<EffectPeriodicityComponent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref EffectPeriodicityComponent c)
        {
            c.Periodicity = 0.0f;
            c.LastApplyingTime = float.MinValue;
        }
    }
}