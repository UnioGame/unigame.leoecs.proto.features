namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect
{
    using System;
    using Components;
    using Effects;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class ShieldEffectConfiguration : EffectConfiguration
    {
        [SerializeField]
        public float shieldValue;
        
        protected override void Compose(ProtoWorld world, ProtoEntity effectEntity)
        {
            var shieldPool = world.GetPool<ShieldEffectComponent>();
            ref var shield = ref shieldPool.Add(effectEntity);
            shield.maxValue = shieldValue;
        }
    }
}