 namespace UniGame.Ecs.Proto.Ability.Aspects
{
    using System;
    using Common.Components;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class AbilityOwnerAspect : EcsAspect
    {
        public ProtoPool<AbilityInHandLinkComponent> AbilityInHandLink;
        public ProtoPool<AbilityMapComponent> AbilityMap;
        public ProtoPool<AbilityInProcessingComponent> AbilityInProcessing;
        public ProtoPool<AbilitySlotComponent> Slot;
        //requests
        
        public ProtoPool<ApplyAbilitySelfRequest> ApplyAbility;
        public ProtoPool<SetInHandAbilitySelfRequest> SetInHandAbility;
        public ProtoPool<ApplyAbilityBySlotSelfRequest> ApplyAbilityBySlot;
    }
}