namespace UniGame.Ecs.Proto.Effects.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Characteristics.AbilityPower.Components;
    using Components;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

    [Serializable]
    public class EffectAspect : EcsAspect
    {
        public ProtoPool<EffectComponent> Effect;
        public ProtoPool<SelfEffectsComponent> SelfEffects;
        public ProtoPool<EffectsListComponent> List;
        public ProtoPool<AbilityPowerComponent> Power;
        public ProtoPool<EffectViewDataComponent> ViewData;
        public ProtoPool<EffectDurationComponent> Duration;
        public ProtoPool<EffectParentComponent> Parent;
        public ProtoPool<EffectPeriodicityComponent> Periodicity;
        public ProtoPool<DelayedEffectComponent> Delayed;
        public ProtoPool<CompletedDelayedEffectComponent> CompletedDelayed;
        public ProtoPool<ViewInstanceTypeComponent> ViewInstanceType;
        
        // Components
        //---Generated Begin---
        public ProtoPool<CompletedDelayedEffectComponent> CompletedDelayedEffectComponent;
        public ProtoPool<DelayedEffectComponent> DelayedEffectComponent;
        public ProtoPool<EffectComponent> EffectComponent;
        public ProtoPool<EffectDurationComponent> EffectDurationComponent;
        public ProtoPool<EffectGlobalRootMarkerComponent> EffectGlobalRootMarkerComponent;
        public ProtoPool<EffectParentComponent> EffectParentComponent;
        public ProtoPool<EffectPeriodicityComponent> EffectPeriodicityComponent;
        public ProtoPool<EffectRootComponent> EffectRootComponent;
        public ProtoPool<EffectRootIdComponent> EffectRootIdComponent;
        public ProtoPool<EffectRootTargetComponent> EffectRootTargetComponent;
        public ProtoPool<EffectRootTransformsComponent> EffectRootTransformsComponent;
        public ProtoPool<EffectsComponent> EffectsComponent;
        public ProtoPool<EffectShowCompleteComponent> EffectShowCompleteComponent;
        public ProtoPool<EffectsListComponent> EffectsListComponent;
        public ProtoPool<EffectsRootDataComponent> EffectsRootDataComponent;
        public ProtoPool<EffectViewComponent> EffectViewComponent;
        public ProtoPool<EffectViewDataComponent> EffectViewDataComponent;
        public ProtoPool<SelfEffectsComponent> SelfEffectsComponent;

        
        //optional
        public ProtoPool<EffectViewComponent> View;
        public ProtoPool<EffectRootTransformsComponent> Transforms;
        public ProtoPool<EntityAvatarComponent> Avatar;
        public ProtoPool<EffectRootIdComponent> EffectRootId;
        public ProtoPool<EffectShowCompleteComponent> ShowComplete;
        public ProtoPool<TransformPositionComponent> Position;
        public ProtoPool<TransformComponent> Transform;
        
        // Events
        //---Generated Begin---
        public ProtoPool<EffectAppliedSelfEvent> EffectAppliedSelfEvent;

        // Requests
        //---Generated Begin---
        public ProtoPool<DestroyEffectSelfRequest> DestroyEffectSelfRequest;
        public ProtoPool<DestroyEffectViewSelfRequest> DestroyEffectViewSelfRequest;
        public ProtoPool<RemoveEffectRequest> RemoveEffectRequest;

        public ProtoPool<CreateEffectSelfRequest> Create;
        public ProtoPool<RemoveEffectRequest> Remove;
        public ProtoPool<DestroyEffectViewSelfRequest> DestroyView;
        public ProtoPool<DestroyEffectSelfRequest> DestroyEffect;
        public ProtoPool<ApplyEffectSelfRequest> Apply;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CreateRequests(
            List<IEffectConfiguration> effects, 
            ProtoPackedEntity source,
            ProtoPackedEntity destination)
        {
            effects.CreateRequests(world, source, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity CreateRequest(IEffectConfiguration effect,
            ref ProtoPackedEntity source,
            ref ProtoPackedEntity destination)
        {
            return effect.CreateRequest(world, ref source, ref destination);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transform GetViewInstance(ProtoEntity entity, int viewInstanceId)
        {
            return entity.GetViewInstance(world, viewInstanceId);
        }
        
    }
}