namespace UniGame.Ecs.Proto.Ability.Behaviours
{
    using System;
    using Common.Components;
    using Leopotam.EcsProto;
    using SubFeatures.FakeTimeline.Data;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Шаг на таймлайне: через delay секунд после применения владелец способности умирает.
    /// </summary>
    [Serializable]
    public sealed class SelfDestructBehaviour : TimelineAbilityBehaviour
    {
        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            world.AddComponent<SelfDestructPlayableComponent>(playableEntity);
        }
    }
}
