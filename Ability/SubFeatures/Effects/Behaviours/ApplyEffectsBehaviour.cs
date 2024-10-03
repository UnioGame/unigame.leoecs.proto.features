namespace UniGame.Ecs.Proto.Ability.SubFeatures.Effects.Behaviours
{
    using System;
    using System.Collections.Generic;
    using FakeTimeline.Data;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using Leopotam.EcsProto;
    using Proto.Effects.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class ApplyEffectsBehaviour : TimelineAbilityBehaviour
    {
        [SerializeReference] 
        public List<IEffectConfiguration> effects = new();
        
        public override void ComposeBehaviour(ProtoWorld world, ProtoEntity abilityEntity, ProtoEntity playableEntity)
        {
            ref var effectsComponent = ref world.GetOrAddComponent<EffectsComponent>(playableEntity);
            effectsComponent.Effects.AddRange(effects);
        }
    }
}