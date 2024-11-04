namespace UniGame.Ecs.Proto.Ability.SubFeatures.Animations.Behaviours
{
    using System;
    using Components;
    using FakeTimeline.Data;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;

#if SPINE_ENABLE
    using Game.Ecs.SpineAnimation.Data.AnimationType;
#endif
    
    [Serializable]
    public class AnimationBehaviour : TimelineAbilityBehaviour
    {
#if SPINE_ENABLE
        public AnimationTypeId animationId;
        public AnimationTypeId nextAnimationId;
#endif
        public float timeScale;

        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            base.ComposeBehaviour(world, abilityEntity, playableEntity);

            if (!abilityEntity.TryGetOwner(world, out var ownerEntity))
            {
                return;
            }
            
#if SPINE_ENABLE
            ref var abilityAnimationComponent = ref world.AddComponent<AbilityAnimationComponent>(playableEntity);
            abilityAnimationComponent.playAnimationId = animationId;
            abilityAnimationComponent.nextPlayAnimationId = nextAnimationId;
            abilityAnimationComponent.timeScale = timeScale;
            abilityAnimationComponent.targetEntity = ownerEntity.PackEntity(world);
#endif
        }
    }
}