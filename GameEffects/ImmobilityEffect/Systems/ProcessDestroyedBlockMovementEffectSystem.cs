namespace UniGame.Ecs.Proto.GameEffects.ImmobilityEffect.Systems
{
    using Components;
    using Effects.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Movement.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class ProcessDestroyedBlockMovementEffectSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ImmobilityEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<DestroyEffectSelfRequest>()
                .End();
        }
        
        public void Run()
        {
            var effectPool = _world.GetPool<EffectComponent>();
            var blockMovementPool = _world.GetPool<ImmobilityComponent>();
            
            foreach (var entity in _filter)
            {
                ref var effect = ref effectPool.Get(entity);
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!blockMovementPool.Has(destinationEntity))
                    continue;

                ref var block = ref blockMovementPool.Get(destinationEntity);
                block.BlockSourceCounter--;
            }
        }
    }
}