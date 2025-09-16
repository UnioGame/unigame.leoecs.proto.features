namespace UniGame.Ecs.Proto.GameEffects.StunEffect.Aspects
{
    using System;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    using TeleportEffect.Components;

    [Serializable]
    public sealed class TeleportEffectAspect : EcsAspect
    {
        public ProtoPool<TeleportEffectComponent> TeleportEffect;
    }
}