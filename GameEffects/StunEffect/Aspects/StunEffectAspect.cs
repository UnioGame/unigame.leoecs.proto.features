namespace UniGame.Ecs.Proto.GameEffects.StunEffect.Aspects
{
    using System;
    using Leopotam.EcsProto;
    using Components;
    using LeoEcs.Bootstrap;

    [Serializable]
    public sealed class StunEffectAspect : EcsAspect
    {
        public ProtoPool<StunEffectComponent> ShieldEffect;
    }
}