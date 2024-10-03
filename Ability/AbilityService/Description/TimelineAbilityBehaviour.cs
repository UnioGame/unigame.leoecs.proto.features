namespace UniGame.Ecs.Proto.Ability.SubFeatures.FakeTimeline.Data
{
    using System;
    using Components;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;

    [Serializable]
    public abstract class TimelineAbilityBehaviour : IAbilityBehaviour
    {
        public float delay;
        
        public void Compose(ProtoWorld world, ProtoEntity abilityEntity)
        {
            var playableEntity = world.NewEntity();
            ref var timelineBehaviourComponent = ref world.AddComponent<TimelinePlayableComponent>(playableEntity);
            timelineBehaviourComponent.delay = delay;

            ref var timelinePrototypeComponent = ref world.GetOrAddComponent<TimelinePrototypeComponent>(abilityEntity);
            timelinePrototypeComponent.Add(playableEntity.PackEntity(world));
            
            ComposeBehaviour(world, abilityEntity, playableEntity);
        }

        public virtual void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
        }
    }
}