namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Represents the aspect of a damage effect.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DamageEffectAspect : EcsAspect
    {
        public ProtoPool<AttackDamageCriticalEffectComponent> AttackDamageCriticalEffect;
        public ProtoPool<AttackDamageEffectComponent> AttackDamageEffect;
        public ProtoPool<DamageEffectComponent> DamageEffect;
        public ProtoPool<DamageEffectRequestCompleteComponent> DamageEffectRequestComplete;
        public ProtoPool<SplashAttackDamageEffectComponent> SplashAttackDamageEffect;
    }
}