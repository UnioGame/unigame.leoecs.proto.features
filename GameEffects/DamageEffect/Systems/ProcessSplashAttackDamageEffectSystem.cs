namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Systems
{
    using System;
    using Characteristics.AttackDamage.Aspects;
    using Characteristics.CriticalMultiplier.Aspects;
    using Characteristics.SplashDamage.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using Gameplay.CriticalAttackChance.Aspects;
    using Gameplay.Damage.Aspects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// deal part of attack damage based on SplashDamageComponent
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessSplashAttackDamageEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DamageAspect _damageAspect;
        private EffectAspect _effectAspect;
        private AttackDamageAspect _attackDamageAspect;
        private SplashDamageAspect _splashDamageAspect;
        private CriticalAttackChanceAspect _criticalAttackChanceAspect;
        private CriticalMultiplierCharacteristicAspect _criticalMultiplierCharacteristicAspect;

        private ProtoIt _filter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<SplashAttackDamageEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);

                if (!effect.Source.Unpack(_world, out var sourceEntity) ||
                    !_splashDamageAspect.SplashDamage.Has(sourceEntity))
                    continue;

                ref var splashDamage = ref _splashDamageAspect.SplashDamage.Get(sourceEntity);
                if (splashDamage.Value == 0) continue;
                ref var attackDamage = ref _attackDamageAspect.AttackDamage.Get(sourceEntity);

                var damage = attackDamage.Value;

                var requestEntity = _world.NewEntity();
                ref var request = ref _damageAspect.ApplyDamage.Add(requestEntity);

                request.Source = effect.Source;
                request.Effector = _world.PackEntity(entity);
                request.Destination = effect.Destination;
                var criticalMultiplier = _criticalAttackChanceAspect.CriticalAttackMarker.Has(sourceEntity)
                    ? 1 + _criticalMultiplierCharacteristicAspect.CriticalMultiplier.Get(sourceEntity).Value / 100
                    : 1;
                request.Value = damage * criticalMultiplier * splashDamage.Value / 100;
            }
        }
    }
}