namespace UniGame.Ecs.Proto.Characteristics.Cooldown.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Aspect that manages cooldown feature.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CooldownAspect : EcsAspect
    {
        public ProtoPool<BaseCooldownComponent> BaseCooldown;
        public ProtoPool<RecalculateCooldownSelfRequest> RecalculateCooldown;
    }
}