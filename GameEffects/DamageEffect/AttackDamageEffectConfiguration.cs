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

    [Serializable]
    public sealed class AttackDamageEffectConfiguration : EffectConfiguration
    {
        #region Inspector

        [SerializeReference]
        public IDamageType DamageType = new PhysicsDamageType();

        #endregion
        protected override void Compose(ProtoWorld world, ProtoEntity effectEntity)
        {
            world.AddComponent<AttackDamageEffectComponent>(effectEntity);
            DamageType.Compose(world, effectEntity);
        }
    }
}