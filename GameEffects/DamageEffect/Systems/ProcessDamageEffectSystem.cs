namespace UniGame.Ecs.Proto.GameEffects.DamageEffect.Systems
{
    using System;
    using Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using Gameplay.Damage.Aspects;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessDamageEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DamageEffectAspect _damageEffectAspect;
        private EffectAspect _effectAspect;
        private DamageAspect _damageAspect;

        private ProtoItExc _filter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<DamageEffectComponent>()
            .Exc<DamageEffectRequestCompleteComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                var abilityPower = 1f;
                if (_effectAspect.Power.Has(entity))
                {
                    ref var abilityDamage = ref _effectAspect.Power.Get(entity);
                    abilityPower = abilityDamage.Value;
                }
                
                ref var damage = ref _damageEffectAspect.DamageEffect.Get(entity);
                var requestEntity = _world.NewEntity();
                ref var request = ref _damageAspect.ApplyDamage.Add(requestEntity);
                request.Source = effect.Source;
                request.Destination = effect.Destination;
                request.Value = damage.Value * abilityPower;
                _damageEffectAspect.DamageEffectRequestComplete.Add(entity);
            }
        }
    }
}