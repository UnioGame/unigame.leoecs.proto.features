namespace UniGame.Ecs.Proto.GameEffects.ImmobilityEffect.Systems
{
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Movement.Aspect;
    using Movement.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    [ECSDI]
    public sealed class ProcessDestroyedBlockMovementEffectSystem : IProtoRunSystem
    {
        private ProtoIt _filter = It
            .Chain<ImmobilityEffectComponent>()
            .Inc<EffectComponent>()
            .Inc<DestroyEffectSelfRequest>()
            .End();

        private EffectAspect _effectAspect;
        private NavMeshAgentAspect _navMeshAgentAspect;
        
        private ProtoWorld _world;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!_navMeshAgentAspect.Immobility.Has(destinationEntity))
                    continue;

                ref var block = ref _navMeshAgentAspect.Immobility
                    .Get(destinationEntity);
                block.BlockSourceCounter--;
            }
        }
    }
}