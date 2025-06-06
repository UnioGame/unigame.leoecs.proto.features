namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Systems
{
    using Characteristics.Shield.Components;
    using Components;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    [ECSDI]
    public sealed class ProcessShieldValueEffectSystem : IProtoRunSystem
    {
        private ProtoItExc _filter = It.Chain<ShieldEffectComponent>()
            .Inc<EffectComponent>()
            .Exc<DestroyEffectSelfRequest>()
            .End();
        
        private ProtoPool<EffectComponent> _effectPool;
        private ProtoPool<ShieldComponent> _shieldPool;
        private ProtoPool<DestroyEffectSelfRequest> _destroyRequestPool;
        
        private ProtoWorld _world;
        
        public void Run()
        {
            var effectPool = _effectPool;
            var shieldPool = _shieldPool;
            var destroyRequestPool = _destroyRequestPool;

            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!shieldPool.Has(destinationEntity))
                {
                    destroyRequestPool.Add(entity);
                }
            }
        }
    }
}