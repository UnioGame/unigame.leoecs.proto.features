namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Systems
{
    using System;
    using Characteristics.Shield.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Aspects;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    [ECSDI]
    public sealed class ProcessShieldEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private EffectAspect _effectAspect;
        private ShieldEffectAspect _shieldEffectAspect;
        private ShieldCharacteristicAspect _shieldCharacteristicAspect;
        
        private ProtoIt _effectFilter = It
            .Chain<ShieldEffectComponent>()
            .Inc<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var effectEntity in _effectFilter)
            {
                ref var effectComponent = ref _effectAspect.Effect.Get(effectEntity);
                if (!effectComponent.Destination.Unpack(_world, out var destinationEntity))
                {
                    continue;
                }

                ref var shieldEffectComponent = ref _shieldEffectAspect.ShieldEffect.Get(effectEntity);
                ref var shieldComponent = ref _shieldCharacteristicAspect.Shield.GetOrAddComponent(destinationEntity);
                shieldComponent.Value += shieldEffectComponent.Value;
            }
        }
    }
}