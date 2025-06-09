namespace UniGame.Ecs.Proto.Gameplay.CriticalAttackChance.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// Aspect for managing critical attack chance feature.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CriticalAttackChanceAspect : EcsAspect
    {
        // Components
        public ProtoPool<CriticalAttackMarkerComponent> CriticalAttackMarker;
        
        // Requests
        public ProtoPool<RecalculateCriticalChanceSelfRequest> Recalculate;
    }
}