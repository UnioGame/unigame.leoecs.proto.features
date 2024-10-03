namespace UniGame.Ecs.Proto.Ability.SubFeatures.Cooldown.Behaviours
{
    using System;
    using Components;
    using FakeTimeline.Data;
    using LeoEcs.Shared.Extensions;
    using LeoEcs.Timer.Components;
    using Leopotam.EcsProto;

    [Serializable]
    public sealed class CooldownBehaviour : TimelineAbilityBehaviour
    {
        public float cooldownValue;

        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            ref var cooldownComponent = ref world.GetOrAddComponent<CooldownComponent>(abilityEntity);
            cooldownComponent.Value = cooldownValue;
            
            world.GetOrAddComponent<CooldownCompleteComponent>(abilityEntity);
            world.AddComponent<CooldownRestartPlayableComponent>(playableEntity);
        }
    }
}