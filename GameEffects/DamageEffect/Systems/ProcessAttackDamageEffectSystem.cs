namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Systems
{
    using System;
    using Characteristics.AttackDamage.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using Gameplay.CriticalAttackChance.Aspects;
    using Gameplay.Damage.Aspects;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAttackDamageEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        private AttackDamageAspect _attackDamageAspect;
        private CriticalAttackChanceAspect _criticalAttackChanceAspect;
        private DamageAspect _damageAspect;
        
        private ProtoIt _filter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<AttackDamageEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);

                if (!effect.Source.Unpack(_world, out var sourceEntity) ||
                    !_attackDamageAspect.AttackDamage.Has(sourceEntity))
                    continue;

                ref var attackDamage = ref _attackDamageAspect.AttackDamage.Get(sourceEntity);

                var damage = attackDamage.Value;

                var requestEntity = _world.NewEntity();
                ref var request = ref _damageAspect.ApplyDamage.Add(requestEntity);

                request.Source = effect.Source;
                request.Effector = _world.PackEntity(entity);
                request.Destination = effect.Destination;
                request.Value = damage;
                request.IsCritical = _criticalAttackChanceAspect.CriticalAttackMarker.Has(sourceEntity);
            }
        }
    }
}