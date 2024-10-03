namespace UniGame.Ecs.Proto.Ability.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using AbilityInventory.Components;
    using Characteristics.Cooldown.Components;
    using Characteristics.Duration.Components;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Components;
    using Components.Requests;
    using Core.Components;
    using Game.Code.Animations.EffectMilestones;
    using Game.Code.Configuration.Runtime.Ability;
    using Game.Code.Configuration.Runtime.Ability.Description;
    using Game.Code.Services.AbilityLoadout.Data;
    using Game.Ecs.Core.Components;
    using Game.Ecs.Input.Components.Evetns;
    using Game.Ecs.Time.Service;
    using Game.Modules.leoecs.proto.features.Ability.AbilityBase.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using GameLayers.Category.Components;
    using GameLayers.Relationship.Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Tools;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Timer.Components;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine.Serialization;

    [Serializable]
    public class AbilityAspect : EcsAspect
    {
        public AbilityOwnerAspect AbilityOwnerAspect;
        [FormerlySerializedAs("LifeTimeAspect")]
        public OwnershipAspect ownershipAspect;
        
        public ProtoWorld World;
        
        // Components
        //---Generated Begin---
        public ProtoPool<AbilityActiveTimeComponent> AbilityActiveTimeComponent;
        public ProtoPool<AbilityBlockedComponent> AbilityBlockedComponent;
        public ProtoPool<AbilityCategoryComponent> AbilityCategoryComponent;
        public ProtoPool<AbilityConfigurationComponent> AbilityConfigurationComponent;
        public ProtoPool<AbilityEffectMilestonesComponent> AbilityEffectMilestonesComponent;
        public ProtoPool<AbilityEquippedComponent> AbilityEquippedComponent;
        public ProtoPool<AbilityEvaluationComponent> AbilityEvaluationComponent;
        public ProtoPool<AbilityIdComponent> AbilityIdComponent;
        public ProtoPool<AbilityInHandComponent> AbilityInHandComponent;
        public ProtoPool<AbilityInHandLinkComponent> AbilityInHandLinkComponent;
        public ProtoPool<AbilityInProcessingComponent> AbilityInProcessingComponent;
        public ProtoPool<AbilityInputComponent> AbilityInputComponent;
        public ProtoPool<AbilityLevelComponent> AbilityLevelComponent;
        public ProtoPool<AbilityLinkComponent> AbilityLinkComponent;
        public ProtoPool<AbilityMapComponent> AbilityMapComponent;
        public ProtoPool<AbilityMetaComponent> AbilityMetaComponent;
        public ProtoPool<AbilityPauseComponent> AbilityPauseComponent;
        public ProtoPool<AbilitySlotComponent> AbilitySlotComponent;
        public ProtoPool<AbilityUnlockComponent> AbilityUnlockComponent;
        public ProtoPool<DefaultAbilityComponent> DefaultAbilityComponent;
        public ProtoPool<DefaultAbilityTargetSlotComponent> DefaultAbilityTargetSlotComponent;
        public ProtoPool<PassiveAbilityComponent> PassiveAbilityComponent;
        public ProtoPool<AbilityContextComponent> AbilityContext;
        
        public ProtoPool<AbilityUnlockComponent> Unlock;
        public ProtoPool<ActiveAbilityComponent> Active;
        public ProtoPool<DefaultAbilityComponent> Default;
        public ProtoPool<AbilitySlotComponent> Slot;
        public ProtoPool<AbilityIdComponent> AbilityId;
        public ProtoPool<NameComponent> Name;
        public ProtoPool<IconComponent> Icon;
        public ProtoPool<CategoryIdComponent> Category;
        public ProtoPool<DurationComponent> Duration;
        public ProtoPool<AnimationDataLinkComponent> AnimationLink;
        public ProtoPool<AbilityInHandLinkComponent> AbilityInHandLink;
        public ProtoPool<RelationshipIdComponent> Relationship;
        public ProtoPool<BaseCooldownComponent> BaseCooldown;
        public ProtoPool<CooldownComponent> Cooldown;
        public ProtoPool<CooldownStateComponent> CooldownState;
        public ProtoPool<RadiusComponent> Radius;
        public ProtoPool<DescriptionComponent> Description;
        public ProtoPool<AbilityConfigurationComponent> Configuration;
        public ProtoPool<AbilityEffectMilestonesComponent> EffectMilestones;
        public ProtoPool<AbilityInHandComponent> InHand;
        public ProtoPool<AbilityPauseComponent> Pause;
        public ProtoPool<EntityAvatarComponent> Avatar;
        public ProtoPool<AbilityMapComponent> AbilityMap;
        public ProtoPool<AbilityEquippedComponent> AbilityEquipped;
        public ProtoPool<AbilityBlockedComponent> Blocked;
        public ProtoPool<AbilityTargetsComponent> Targets;
        
        public ProtoPool<AbilityEvaluationComponent> Evaluate;
        
        //is ability in use
        public ProtoPool<AbilityUsingComponent> AbilityUsing;
        
        // Requests
        //---Generated Begin---
        public ProtoPool<AbilityValidationSelfRequest> AbilityValidationSelfRequest;
        public ProtoPool<ActivateAbilityByIdRequest> ActivateAbilityByIdRequest;
        public ProtoPool<ActivateAbilityRequest> ActivateAbilityRequest;
        public ProtoPool<ActivateAbilitySelfRequest> ActivateAbilitySelfRequest;
        public ProtoPool<ApplyAbilityByIdSelfRequest> ApplyAbilityByIdSelfRequest;
        public ProtoPool<ApplyAbilityBySlotSelfRequest> ApplyAbilityBySlotSelfRequest;
        public ProtoPool<ApplyAbilityEffectsSelfRequest> ApplyAbilityEffectsSelfRequest;
        public ProtoPool<ApplyAbilitySelfRequest> ApplyAbilitySelfRequest;
        public ProtoPool<CreateAbilityRequest> CreateAbilityRequest;
        public ProtoPool<EquipAbilityIdSelfRequest> EquipAbilityIdSelfRequest;
        public ProtoPool<EquipAbilityIdToChampionRequest> EquipAbilityIdToChampionRequest;
        public ProtoPool<EquipAbilityNameSelfRequest> EquipAbilityNameSelfRequest;
        public ProtoPool<PauseAbilityRequest> PauseAbilityRequest;
        public ProtoPool<RemovePauseAbilityRequest> RemovePauseAbilityRequest;
        public ProtoPool<ResetAbilityCooldownSelfRequest> ResetAbilityCooldownSelfRequest;
        public ProtoPool<RestartAbilityCooldownSelfRequest> RestartAbilityCooldownSelfRequest;
        public ProtoPool<SetAbilityBaseCooldownSelfRequest> SetAbilityBaseCooldownSelfRequest;
        public ProtoPool<SetInHandAbilityBySlotSelfRequest> SetInHandAbilityBySlotSelfRequest;
        public ProtoPool<SetInHandAbilitySelfRequest> SetInHandAbilitySelfRequest;

        
        public ProtoPool<ApplyAbilityEffectsSelfRequest> ApplyAbilityEffects;
        public ProtoPool<ActivateAbilityRequest> ActivateAbilityOnTarget;
        public ProtoPool<SetInHandAbilityBySlotSelfRequest> SetInHandAbilityBySlot;
        public ProtoPool<ActivateAbilityByIdRequest> ActivateAbilityById;
        //complete ability
        public ProtoPool<CompleteAbilitySelfRequest> CompleteAbility;
        
        public ProtoPool<AbilityValidationSelfRequest> Validate;
        public ProtoPool<SetAbilityBaseCooldownSelfRequest> SetBaseCooldown;
        public ProtoPool<RecalculateCooldownSelfRequest> RecalculateCooldown;
        
        //activate ability
        public ProtoPool<ApplyAbilityBySlotSelfRequest> ActivateAbilityBySlot;
        
        //requests
        public ProtoPool<EquipAbilityIdSelfRequest> EquipAbilityIdRequest;
        public ProtoPool<EquipAbilityReferenceSelfRequest> EquipAbilityReferenceRequest;
        public ProtoPool<PauseAbilityRequest> PauseAbility;
        public ProtoPool<RemovePauseAbilityRequest> RemovePauseAbility;
        
        // Events
        //---Generated Begin---
        public ProtoPool<AbilityCompleteSelfEvent> AbilityCompleteSelfEvent;

        public ProtoPool<AbilityStartUsingSelfEvent> UsingEvent;
        public ProtoPool<AbilityCompleteSelfEvent> CompleteEvent;
        public ProtoPool<AbilityVelocityEvent> AbilityVelocityEvent;
        public ProtoPool<AbilityCellVelocityEvent> AbilityCellVelocityEvent;
        public ProtoPool<AbilityUnlockEvent> AbilityUnlockEvent;
        
        private ProtoEntity _invalidEntity = (ProtoEntity)(-1);
        private ProtoEntity _invalidAbility = (ProtoEntity)(-1);

        public ProtoIt AbilityFilter = It
            .Chain<AbilityIdComponent>()
            .Inc<ActiveAbilityComponent>()
            .Inc<OwnerLinkComponent>()
            .End();

        public ProtoIt ActiveAbilityFilter = It
            .Chain<ActiveAbilityComponent>()
            .Inc<AbilityIdComponent>()
            .Inc<OwnerLinkComponent>()
            .End();
        
        public ProtoIt AbilityInUseFilter = It
            .Chain<AbilityIdComponent>()
            .Inc<AbilityUsingComponent>()
            .Inc<ActiveAbilityComponent>()
            .Inc<OwnerLinkComponent>()
            .End();

        public ProtoIt ExistsAbilityFilter = It
            .Chain<AbilityIdComponent>()
            .Inc<OwnerLinkComponent>()
            .Inc<ActiveAbilityComponent>()
            .End();
        
        public void BuildAbility(
            ProtoEntity abilityEntity,
            ref ProtoPackedEntity ownerEntity,
            AbilityConfiguration abilityConfiguration,
            ref AbilityBuildData buildData)
        {
            var packedAbility = World.PackEntity(abilityEntity);
            ref var slotComponent = ref Slot.GetOrAddComponent(abilityEntity);
            ref var abilityIdComponent = ref AbilityId.GetOrAddComponent(abilityEntity);
            ref var cooldownComponent = ref Cooldown.GetOrAddComponent(abilityEntity);
            ref var cooldownStateComponent = ref CooldownState.GetOrAddComponent(abilityEntity);
            ref var durationComponent = ref world.GetOrAddComponent<DurationComponent>(abilityEntity);

            ownerEntity.AddChild(abilityEntity, world);
            
            slotComponent.SlotType = buildData.Slot;
            abilityIdComponent.AbilityId = buildData.AbilityId;

//             if (abilityConfiguration.useAnimation)
//             {
// #if UNITY_EDITOR
//                 if (abilityLink == null || !abilityLink.RuntimeKeyIsValid())
//                 {
//                     Debug.LogError($"Missing ability animation link FOR {abilityConfiguration.name}");
//                 }
// #endif
//                 ComposeAbilityAnimationAsync(_world, ownerEntity, packedAbility, abilityLink).Forget();
//             }
//             else
//             {
//                 ref var durationComponent = ref _duration.GetOrAddComponent(abilityEntity);
//                 durationComponent.Value = abilityConfiguration.duration;
//                 ref var milestonesComponent =
//                     ref _world.GetOrAddComponent<AbilityEffectMilestonesComponent>(abilityEntity);
//                 milestonesComponent.Milestones = new[]
//                 {
//                     new EffectMilestone { Time = 0f }
//                 };
//             }

            foreach (var abilityBehaviour in abilityConfiguration.abilityBehaviours)
                abilityBehaviour.Compose(World, abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAbilityById(ref ProtoPackedEntity ownerEntity, AbilityId abilityId,
            out ProtoEntity resultAbility)
        {
            foreach (var abilityEntity in AbilityFilter)
            {
                ref var ownerLinkComponent = ref ownershipAspect.OwnerLink.Get(abilityEntity);
                if (!ownerEntity.Equals(ownerLinkComponent.Value)) continue;

                ref var abilityIdComponent = ref AbilityId.Get(abilityEntity);
                if (abilityId != abilityIdComponent.AbilityId) continue;

                resultAbility = abilityEntity;
                return true;
            }

            resultAbility = _invalidEntity;
            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetExistsAbility(int abilityId, ref ProtoPackedEntity targetEntity)
        {
            foreach (var abilityEntity in ExistsAbilityFilter)
            {
                ref var abilityIdComponent = ref AbilityId.Get(abilityEntity);
                ref var ownerLinkComponent = ref ownershipAspect.OwnerLink.Get(abilityEntity);

                if (abilityIdComponent.AbilityId != abilityId ||
                    !ownerLinkComponent.Value.Equals(targetEntity))
                {
                    continue;
                }

                return (int)abilityEntity;
            }

            return -1;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity GetActivatedAbility(ProtoEntity ownerEntity)
        {
            ref var abilityMap = ref AbilityMap.Get(ownerEntity);
            foreach (var abilityMapAbilityEntity in abilityMap.AbilitySlots)
            {
                ref var packedAbility = ref abilityMapAbilityEntity.Value;
                if (!packedAbility.Unpack(World, out var abilityEntity)) continue;
                
                if (!AbilityUsing.Has(abilityEntity)) continue;
                if (CompleteAbility.Has(abilityEntity)) continue;
                return  abilityEntity;
            }

            return default;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ChangeInHandAbility(ProtoEntity entity, ProtoEntity newAbility)
        {
            ref var targetInHandComponent = ref AbilityInHandLink.Get(entity);
            var abilityEntity = targetInHandComponent.AbilityEntity;

            if (abilityEntity.Unpack(World, out var previousAbility))
            {
                if (previousAbility.Equals(newAbility))
                    return;

                InHand.TryRemove(previousAbility);
            }

            if (!InHand.Has(newAbility))
                InHand.Add(newAbility);

            abilityEntity = World.PackEntity(newAbility);
            targetInHandComponent.AbilityEntity = abilityEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAbilityCooldownPassed(ProtoEntity abilityEntity)
        {
            //проверяем кулдаун абилки, если он не прошел - игнорируем
            if (!World.EntityHasAll<CooldownComponent, CooldownStateComponent>(abilityEntity))
                return true;

            ref var coolDownComponent = ref Cooldown.Get(abilityEntity);
            ref var coolDownStateComponent = ref World.GetComponent<CooldownStateComponent>(abilityEntity);
            var timePassed = (GameTime.Time - coolDownStateComponent.LastTime) - coolDownComponent.Value;
            return timePassed > 0;
        }


#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoPackedEntity GetAbilityBySlot(ProtoEntity ownerEntity, int slotId)
        {
            if(AbilityMap.Has(ownerEntity) == false)
                return default;
            
            ref var abilityMapComponent = ref AbilityMap.Get(ownerEntity);
            var abilityMap = abilityMapComponent.AbilitySlots;

            abilityMap.TryGetValue(slotId,out var packedAbility);
            return packedAbility;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetAbilitySlot(ProtoEntity ownerEntity, ProtoEntity abilityEntity)
        {
            ref var abilityMap = ref AbilityMap.Get(ownerEntity);

            foreach (var abilityPair in abilityMap.AbilitySlots)
            {
                ref var abilityPacked = ref abilityPair.Value;
                if (!abilityPacked.Unpack(World, out var targetAbility)) continue;
                if (abilityEntity.Equals(targetAbility)) return abilityPair.Key;
            }

            return AbilitySlotId.EmptyAbilitySlot;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAnyAbilityInUse(ProtoEntity entity)
        {
            if (!AbilityMap.Has(entity)) return false;

            ref var abilityMapComponent = ref AbilityMap.Get(entity);
            var abilityMap = abilityMapComponent.AbilitySlots;
            
            foreach (var pair in abilityMap)
            {
                var packedAbility = pair.Value;
                if (!packedAbility.Unpack(World, out var abilityEntity))
                    continue;

                if (AbilityUsing.Has(abilityEntity)) return true;
            }
            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoPackedEntity GetNonDefaultAbilityInUse(ProtoEntity entity)
        {
            if (!AbilityOwnerAspect.AbilityInProcessing.Has(entity))
                return default;

            ref var processingComponent = ref AbilityOwnerAspect
                .AbilityInProcessing.Get(entity);

            if (processingComponent.IsDefault) return default;

            return processingComponent.Ability;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity EquipAbilityById(ref ProtoPackedEntity owner, ref AbilityId abilityId)
        {
            return EquipAbilityById(ref owner, abilityId, AbilitySlotId.EmptyAbilitySlot);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity EquipAbilityById(ref ProtoPackedEntity owner, AbilityId abilityId,
            AbilitySlotId slot, bool isDefault = false)
        {
            var activeAbility = GetActiveAbility(ref owner, ref abilityId);
            if ((int)activeAbility > 0) return activeAbility;

            var requestEntity = World.NewEntity();
            ref var request = ref EquipAbilityIdRequest.Add(requestEntity);
            request.AbilityId = abilityId;
            request.AbilitySlot = slot;
            request.Owner = owner;
            request.IsDefault = isDefault;

            return requestEntity;
        }


#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity EquipAbilityByReference(ref ProtoPackedEntity owner,
            AbilityConfiguration configuration,
            AbilitySlotId slot, bool isDefault = false)
        {
            var requestEntity = World.NewEntity();
            ref var request = ref EquipAbilityReferenceRequest.Add(requestEntity);
            request.AbilitySlot = slot;
            request.Owner = owner;
            request.IsDefault = isDefault;
            request.Reference = configuration;

            return requestEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity GetActiveAbility(ref ProtoPackedEntity owner, ref AbilityId abilityId)
        {
            foreach (var abilityEntity in AbilityFilter)
            {
                ref var ownerLinkComponent = ref ownershipAspect.OwnerLink.Get(abilityEntity);
                if (!owner.Equals(ownerLinkComponent.Value))
                {
                    continue;
                }

                ref var abilityIdComponent = ref AbilityId.Get(abilityEntity);
                if (abilityId != abilityIdComponent.AbilityId) continue;

                return abilityEntity;
            }

            return default;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IsActiveAbilityEntity(ref ProtoPackedEntity owner, ProtoEntity targetAbility)
        {
            foreach (var abilityEntity in AbilityFilter)
            {
                ref var ownerLinkComponent = ref ownershipAspect.OwnerLink.Get(abilityEntity);
                if (!owner.Equals(ownerLinkComponent.Value))
                {
                    continue;
                }

                if (!targetAbility.Equals(abilityEntity))
                {
                    continue;
                }
                
                return (int)abilityEntity;
            }

            return -1;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeAbilitySpecification(AbilitySpecification specification, ProtoEntity abilityEntity)
        {
            ref var radiusComponent = ref Radius.GetOrAddComponent(abilityEntity);
            ref var category = ref Category.GetOrAddComponent(abilityEntity);
            ref var relationship = ref Relationship.GetOrAddComponent(abilityEntity);
            ref var abilityCooldownComponent = ref CooldownState.GetOrAddComponent(abilityEntity);
            ref var cooldownComponent = ref Cooldown.GetOrAddComponent(abilityEntity);
            ref var baseCooldown = ref BaseCooldown.GetOrAddComponent(abilityEntity);

            baseCooldown.Value = specification.Cooldown;
            cooldownComponent.Value = specification.Cooldown;
            abilityCooldownComponent.LastTime = GameTime.Time - specification.Cooldown;
            radiusComponent.Value = specification.Radius;
            relationship.Value = specification.RelationshipId;
            category.Value = specification.CategoryId;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ComposeEffectMilestones(EffectMilestonesData milestonesInfo,
            float duration,
            ProtoEntity abilityEntity)
        {
            if (milestonesInfo == null) return;

            ref var effectMilestones = ref World.GetOrAddComponent<AbilityEffectMilestonesComponent>(abilityEntity);

            var effects = milestonesInfo.effectMilestones;
            if (effects.Count == 0)
            {
                effectMilestones.Milestones = new[]
                {
                    new EffectMilestone { Time = duration }
                };

                return;
            }

            effectMilestones.Milestones = new EffectMilestone[effects.Count];

            for (var i = 0; i < effectMilestones.Milestones.Length; i++)
            {
                var sourceMilestone = effects[i];
                var cloneMilestone = sourceMilestone.Clone();
                effectMilestones.Milestones[i] = cloneMilestone;
            }
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbility(ProtoEntity entity, ProtoEntity abilityEntity)
        {
            var packedAbilityEntity = World.PackEntity(abilityEntity);
            ref var setInHand = ref World.GetOrAddComponent<SetInHandAbilitySelfRequest>(entity);
            setInHand.Value = packedAbilityEntity;

            ref var abilityUseRequest = ref World.GetOrAddComponent<ApplyAbilitySelfRequest>(entity);
            abilityUseRequest.Value = packedAbilityEntity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbility( ProtoEntity abilityEntity)
        {
            if (!World.HasComponent<OwnerComponent>(abilityEntity)) return;

            ref var ownerLinkComponent = ref ownershipAspect.OwnerLink.Get(abilityEntity);
            if (!ownerLinkComponent.Value.Unpack(World, out var ownerEntity))
            {
                return;
            }

            ActivateAbility(ownerEntity, abilityEntity);
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilitySlot( int entity, int slot)
        {
            ref var setInHand = ref World.GetOrAddComponent<SetInHandAbilityBySlotSelfRequest>(entity);
            setInHand.AbilityCellId = slot;

            var abilityInputPool = World.GetPool<ApplyAbilityBySlotSelfRequest>();
            ref var abilityUseRequest = ref abilityInputPool.GetOrAddComponent(entity);
            abilityUseRequest.AbilitySlot = slot;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ActivateAbilityId(ref ProtoPackedEntity entity, int abilityId)
        {
            var requestEntity = World.NewEntity();
            ref var activateRequest = ref World.GetOrAddComponent<ActivateAbilityByIdRequest>(requestEntity);
            activateRequest.AbilityId = abilityId;
            activateRequest.Target = entity;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public bool TryGetInHandAbility( ProtoEntity entity, out ProtoEntity abilityEntity)
        {
            abilityEntity = _invalidEntity;
            var abilityInHandPool = World.GetPool<AbilityInHandLinkComponent>();
            if (!abilityInHandPool.Has(entity))
                return false;

            ref var inHandAbility = ref abilityInHandPool.Get(entity);
            if (!inHandAbility.AbilityEntity.Unpack(World, out var ability))
                return false;

            abilityEntity = ability;
            return true;
        }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        public bool TryGetDefaultAbility(ProtoEntity entity, out ProtoEntity abilityEntity)
        {
            abilityEntity = _invalidEntity;
            var abilityMapPool = World.GetPool<AbilityMapComponent>();
            var defaultPool = World.GetPool<DefaultAbilityComponent>();

            if (!abilityMapPool.Has(entity)) return false;

            ref var map = ref abilityMapPool.Get(entity);

            foreach (var abilityPair in map.AbilitySlots)
            {
                ref var mapAbilityEntity = ref abilityPair.Value;
                if (!mapAbilityEntity.Unpack(World, out var abilityEntityValue)) continue;
                if (!defaultPool.Has(abilityEntityValue)) continue;
                abilityEntity = abilityEntityValue;
                return true;
            }

            return false;
        }

#if ENABLE_IL2CPP
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProtoEntity TryGetAbility(ProtoEntity entity, int slot)
        {
            if (!AbilityOwnerAspect.AbilityMap.Has(entity)) return default;

            ref var map = ref AbilityOwnerAspect.AbilityMap.Get(entity);
            if (!map.AbilitySlots.TryGetValue(slot, out var packedEntity))
                return default;

            return packedEntity.Unpack(World, out var ability)
                ? ability
                : default;
        }
    }
}