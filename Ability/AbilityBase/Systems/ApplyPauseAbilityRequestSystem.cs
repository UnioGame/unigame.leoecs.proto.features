namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Add the ability suspension component to the ability
    /// </summary>

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyPauseAbilityRequestSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _filter;

        private AbilityAspect _abilityAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PauseAbilityRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var pauseAbilityRequest = ref _abilityAspect.PauseAbilityRequest.Get(entity);
                if (!pauseAbilityRequest.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                _abilityAspect.Pause.TryAdd(abilityEntity);
            }
        }
    }
}