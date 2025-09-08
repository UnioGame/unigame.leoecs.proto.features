namespace UniGame.Ecs.Proto.GameEffects.HealingEffect.Systems
{
    using System;
    using Aspects;
    using Characteristics.Health.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
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
    public sealed class ProcessHealingEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private HealthAspect _healthAspect;
        private EffectAspect _effectAspect;
        private HealingEffectAspect _healingEffectAspect;

        private ProtoIt _effectFilter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<HealingEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var effectEntity in _effectFilter)
            {
                ref var effectComponent = ref _effectAspect.Effect.Get(effectEntity);
                ref var healingEffectComponent = ref _healingEffectAspect.HealingEffect.Get(effectEntity);

                var healthRequestEntity = _world.NewEntity();
                ref var healthRequest = ref _healthAspect.ChangeBase.Add(healthRequestEntity);
                healthRequest.Source = effectComponent.Source;
                healthRequest.Target = effectComponent.Destination;
                healthRequest.Value = healingEffectComponent.Value;

                var madeHealEntity = _world.NewEntity();
                ref var madeHealEvent = ref _healingEffectAspect.Made.Add(madeHealEntity);
                madeHealEvent.Source = effectComponent.Source;
                madeHealEvent.Destination = effectComponent.Destination;
                madeHealEvent.Value = healingEffectComponent.Value;
            }
        }
    }
}