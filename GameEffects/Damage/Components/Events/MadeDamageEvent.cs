namespace UniGame.Ecs.Proto.Gameplay.Damage.Components.Events
{
    using System;
    using LeoEcs.Proto;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Event that indicates a damage has been made.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MadeDamageEvent : IProtoAutoReset<MadeDamageEvent>
    {
        public float Value;
        public bool IsCritical;
        
        public ProtoPackedEntity Source;
        public ProtoPackedEntity Destination;
        
        public void SetHandlers(IProtoPool<MadeDamageEvent> pool) => pool.SetResetHandler(AutoReset);
        
        public static void AutoReset(ref MadeDamageEvent c)
        {
            c.Value = 0.0f;
            c.IsCritical = false;
        }
    }
}