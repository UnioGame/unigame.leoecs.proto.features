namespace UniGame.Ecs.Proto.Ability.Common.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using SubFeatures.FakeTimeline.Aspects;
    using SubFeatures.FakeTimeline.Components.Requests;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.Proto.Ownership;

    /// <summary>
    /// Шаг способности «самоуничтожение»: владелец способности умирает (PrepareToDeath, дальше обычная
    /// смерть и деспавн). Стоит после наложения эффектов, поэтому урон этого же шага уже прошел.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelfDestructAbilityOwnerSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;
        private TimelineAspect _timelineAspect;
        private OwnershipAspect _ownershipAspect;

        private ProtoIt _filter = It
            .Chain<SelfDestructPlayableComponent>()
            .Inc<ExecuteTimelinePlayableRequest>()
            .End();

        public void Run()
        {
            foreach (var playableEntity in _filter)
            {
                ref var executeComponent = ref _timelineAspect.TimelineExecute.Get(playableEntity);
                if (!executeComponent.TimelineContextEntity.Unpack(_world, out var contextEntity))
                    continue;

                ref var abilityContextComponent = ref _abilityAspect.AbilityContext.Get(contextEntity);
                if (!abilityContextComponent.abilityOwner.Unpack(_world, out var ownerEntity))
                    continue;

                _ownershipAspect.Kill(ownerEntity);
            }
        }
    }
}
