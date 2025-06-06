namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CreateApplyAbilityEffectsRequestSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;

        private AbilityAspect _abilityAspect;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AbilityEffectMilestonesComponent>()
                .Inc<AbilityEvaluationComponent>()
                .Inc<AbilityUsingComponent>()
                .End();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var milestones = ref _abilityAspect.AbilityEffectMilestonesComponent.Get(entity);
                ref var evaluation = ref _abilityAspect.AbilityEvaluationComponent.Get(entity);

                var evaluationTime = evaluation.EvaluateTime;
                for (var i = 0; i < milestones.Milestones.Length; i++)
                {
                    ref var milestone = ref milestones.Milestones[i];
                    if (milestone.IsApplied)
                        continue;

                    if (milestone.Time > evaluationTime && !Mathf.Approximately(milestone.Time, evaluationTime))
                        continue;

                    milestone.IsApplied = true;
                    _abilityAspect.ApplyAbilityEffectsSelfRequest.Add(entity);
                }
            }
        }
    }
}