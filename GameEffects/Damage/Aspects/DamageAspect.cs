namespace UniGame.Ecs.Proto.Gameplay.Damage.Aspects
{
    using System;
    using Characteristics.Dodge.Components;
    using Components.Events;
    using Components.Request;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Represents a damage aspect in the gameplay feature.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class DamageAspect : EcsAspect
    {
        // Components 
        public ProtoPool<BlockableDamageComponent> BlockableDamage;

        // Requests
        public ProtoPool<ApplyDamageRequest> ApplyDamage;

        // Events
        public ProtoPool<MadeDamageEvent> MadeDamage;
        public ProtoPool<CriticalDamageEvent> CriticalDamage;
        public ProtoPool<BlockedDamageEvent> BlockedDamage;
    }
}