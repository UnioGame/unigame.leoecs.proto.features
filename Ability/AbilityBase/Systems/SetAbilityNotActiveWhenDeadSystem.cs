namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using Aspects;
    using Components;
    using Game.Ecs.Core.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    [ECSDI]
    public sealed class SetAbilityNotActiveWhenDeadSystem : IProtoRunSystem,IProtoInitSystem
    {
        private EcsFilter _filter;
        private ProtoWorld _world;

        private AbilityAspect _abilityAspect;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<PrepareToDeathComponent>()
                .Inc<AbilityMapComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var map = ref _abilityAspect.AbilityMap.Get(entity);
                foreach (var packedEntity in map.Abilities)
                {
                    if(!packedEntity.Unpack(_world, out var abilityEntity))
                        continue;
                    
                    if(_abilityAspect.Active.Has(abilityEntity))
                        _abilityAspect.Active.Del(abilityEntity);
                }
            }
        }
    }
}