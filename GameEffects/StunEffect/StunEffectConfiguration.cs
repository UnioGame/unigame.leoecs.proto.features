namespace UniGame.Ecs.Proto.GameEffects.StunEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public sealed class StunEffectConfiguration : EffectConfiguration
    {
        protected override void Compose(ProtoWorld world, ProtoEntity effectEntity)
        {
            world.AddComponent<StunEffectComponent>(effectEntity);
        }
    }
}