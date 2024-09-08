namespace UniGame.Ecs.Proto.GameEffects.HealingEffect.Aspects
{
    using System;
    using Components;
    using Events.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// Aspect for the HealingEffect system in the game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class HealingEffectAspect : EcsAspect
    {
        public ProtoPool<HealingEffectComponent> HealingEffect;
        public ProtoPool<MadeHealEvent> Made;
    }
}