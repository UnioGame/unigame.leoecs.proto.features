namespace UniGame.Ecs.Proto.GameEffects.HealingEffect.Events.Components
{
    using System;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Event where heal is made.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MadeHealEvent
    {
        public ProtoPackedEntity Destination;
        public ProtoPackedEntity Source;
        public float Value;
    }
}