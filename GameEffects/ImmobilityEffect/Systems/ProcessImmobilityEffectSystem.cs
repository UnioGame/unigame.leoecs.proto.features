namespace UniGame.Ecs.Proto.GameEffects.ImmobilityEffect.Systems
{
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Movement.Aspect;
    using UniGame.LeoEcs.Shared.Extensions;

    [ECSDI]
    public sealed class ProcessImmobilityEffectSystem : IProtoRunSystem
    {
        private ProtoIt _filter = It
            .Chain<ImmobilityEffectComponent>()
            .Inc<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .End();
        
        private NavMeshAgentAspect _navMeshAgentAspect; 
        private EffectAspect _effectAspect;
        
        private ProtoWorld _world;
        
        
        public void Run()
        {

            foreach (var entity in _filter)
            {
                ref var effect = ref _effectAspect.Effect.Get(entity);
                
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                ref var block = ref _navMeshAgentAspect
                    .Immobility
                    .GetOrAddComponent(destinationEntity);
                
                block.BlockSourceCounter++;
            }
        }
    }
}