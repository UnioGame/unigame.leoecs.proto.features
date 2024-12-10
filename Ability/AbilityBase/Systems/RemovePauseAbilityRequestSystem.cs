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
    /// Remove the ability pause component
    /// </summary>

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemovePauseAbilityRequestSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private EcsFilter _filter;

        private AbilityAspect _abilityAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RemovePauseAbilityRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var pauseAbilityRequest = ref _abilityAspect.RemovePauseAbilityRequest.Get(entity);
                if (!pauseAbilityRequest.AbilityEntity.Unpack(_world, out var abilityEntity))
                    continue;
                _abilityAspect.AbilityPauseComponent.TryRemove(abilityEntity);
            }
        }
    }
}