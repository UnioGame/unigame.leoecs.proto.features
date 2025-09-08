namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Aspects
{
    using System;
    using Leopotam.EcsProto;
    using Components;
    using LeoEcs.Bootstrap;

    [Serializable]
    public sealed class ShieldEffectAspect : EcsAspect
    {
        public ProtoPool<ShieldEffectComponent> ShieldEffect;
    }
}