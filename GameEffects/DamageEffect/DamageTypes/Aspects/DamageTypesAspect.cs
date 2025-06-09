namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.DamageTypes.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Aspect for handling different types of damage in a game effect.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DamageTypesAspect : EcsAspect
    {
        public ProtoPool<MagicDamageComponent> MagicDamage;
        public ProtoPool<PhysicsDamageComponent> PhysicsDamage;
    }
}