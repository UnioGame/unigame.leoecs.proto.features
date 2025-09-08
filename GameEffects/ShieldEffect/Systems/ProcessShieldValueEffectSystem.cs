namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Systems
{
    using System;
    using Aspects;
    using Characteristics.Shield.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    [ECSDI]
    public sealed class ProcessShieldValueEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private ShieldEffectAspect _stunEffectAspect;
        private EffectAspect _effectAspect;
        private ShieldCharacteristicAspect _shieldCharacteristicAspect;
        
        private ProtoItExc _effectFilter = It
            .Chain<ShieldEffectComponent>()
            .Inc<EffectComponent>()
            .Exc<DestroyEffectSelfRequest>()
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

                if (!_shieldCharacteristicAspect.Shield.Has(destinationEntity))
                {
                    _effectAspect.DestroyEffectSelfRequest.Add(effectEntity);
                }
            }
        }
    }
}