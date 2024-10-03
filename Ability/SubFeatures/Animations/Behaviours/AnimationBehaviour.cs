namespace UniGame.Ecs.Proto.Ability.SubFeatures.Animations.Behaviours
{
    using System;
    using Components;
    using FakeTimeline.Data;
    using Game.Ecs.SpineAnimation.Data.AnimationType;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;

    [Serializable]
    public class AnimationBehaviour : TimelineAbilityBehaviour
    {
        public AnimationTypeId animationId;
        public AnimationTypeId nextAnimationId;
        public float timeScale;

        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            base.ComposeBehaviour(world, abilityEntity, playableEntity);

            if (!abilityEntity.TryGetOwner(world, out var ownerEntity))
            {
                return;
            }
            
            ref var abilityAnimationComponent = ref world.AddComponent<AbilityAnimationComponent>(playableEntity);
            abilityAnimationComponent.playAnimationId = animationId;
            abilityAnimationComponent.nextPlayAnimationId = nextAnimationId;
            abilityAnimationComponent.timeScale = timeScale;
            abilityAnimationComponent.targetEntity = ownerEntity.PackEntity(world);
        }
    }
}