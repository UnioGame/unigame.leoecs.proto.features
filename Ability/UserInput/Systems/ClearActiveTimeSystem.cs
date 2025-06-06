namespace UniGame.Ecs.Proto.Ability.UserInput.Systems
{
    using Common.Components;
    using Game.Ecs.Input.Components;
    using Game.Ecs.Input.Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    public sealed class ClearActiveTimeSystem : IProtoRunSystem
    {
        private ProtoItExc _filter = It.Chain<AbilityMapComponent>()
                    .Inc<UserInputTargetComponent>()
                    .Exc<AbilityUpInputRequest>()
                    .End();
        
        private ProtoWorld _world;
        private ProtoPool<AbilityActiveTimeComponent> _activateTimePool;
        private ProtoPool<AbilityMapComponent> _abilityMapPool;

        public void Run()
        {
            var activateTimePool = _activateTimePool;
            var abilityMapPool = _abilityMapPool;
            
            foreach (var entity in _filter)
            {
                ref var abilityMap = ref abilityMapPool.Get(entity);
                foreach (var packedEntity in abilityMap.Abilities)
                {
                    if(!packedEntity.Unpack(_world, out var abilityEntity) || !activateTimePool.Has(abilityEntity))
                        continue;

                    ref var activateTime = ref activateTimePool.Get(abilityEntity);
                    activateTime.Time = 0.0f;
                }
            }
        }
    }
}