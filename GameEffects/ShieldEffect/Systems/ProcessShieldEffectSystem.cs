namespace UniGame.Ecs.Proto.GameEffects.ShieldEffect.Systems
{
    using Characteristics.Shield.Components;
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessShieldEffectSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ShieldEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .End();
        }

        public void Run()
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var shieldEffectPool = _world.GetPool<ShieldEffectComponent>();
            var shieldPool = _world.GetPool<ShieldComponent>();

            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if (!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                ref var shieldEffect = ref shieldEffectPool.Get(entity);
                ref var shield = ref shieldPool.GetOrAddComponent(destinationEntity);
                shield.Value += shieldEffect.MaxValue;
            }
        }
    }
}