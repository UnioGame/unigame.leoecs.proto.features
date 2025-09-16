namespace UniGame.Ecs.Proto.GameEffects.DamageEffect
{
    using System;
    using Components;
    using DamageTypes;
    using DamageTypes.Abstracts;
    using Effects;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class DamageEffectConfiguration : EffectConfiguration
    {
        #region Inspector

        [SerializeReference]
        public IDamageType DamageType = new PhysicsDamageType();

        #endregion
        
        [FormerlySerializedAs("_damageValue")]
        [Min(0.0f)]
        public float damageValue;
        
        protected override void Compose(ProtoWorld world, ProtoEntity effectEntity)
        {
            var damagePool = world.GetPool<DamageEffectComponent>();
            ref var damage = ref damagePool.Add(effectEntity);
            damage.Value = damageValue;
            DamageType.Compose(world, effectEntity);
        }
    }
}