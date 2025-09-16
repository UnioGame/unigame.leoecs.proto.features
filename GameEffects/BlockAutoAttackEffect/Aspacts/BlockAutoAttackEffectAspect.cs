namespace UniGame.Ecs.Proto.Gameplay.Damage.Aspects
{
    using System;
    using GameEffects.BlockAutoAttackEffect.Components;
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
    public class BlockAutoAttackEffectAspect : EcsAspect
    {
        // Components 
        public ProtoPool<BlockAutoAttackEffectComponent> BlockAutoAttackEffect;
        public ProtoPool<BlockAutoAttackEffectReadyComponent> BlockAutoAttackEffectReady;

    }
}