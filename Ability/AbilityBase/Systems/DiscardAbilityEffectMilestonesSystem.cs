namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public sealed class DiscardAbilityEffectMilestonesSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<CompleteAbilitySelfRequest>()
                .Inc<AbilityEffectMilestonesComponent>()
                .End();
        }
        
        public void Run()
        {
            var milestonesPool = _world.GetPool<AbilityEffectMilestonesComponent>();

            foreach (var entity in _filter)
            {
                ref var milestones = ref milestonesPool.Get(entity);
                for (var i = 0; i < milestones.Milestones.Length; i++)
                {
                    ref var milestone = ref milestones.Milestones[i];
                    milestone.IsApplied = false;
                }
            }
        }
    }
}