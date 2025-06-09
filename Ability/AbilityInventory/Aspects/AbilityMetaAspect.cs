namespace UniGame.Ecs.Proto.AbilityInventory.Aspects
{
    using System;
    using Ability.Common.Components;
    using Ability.Components;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;

    [Serializable]
    [ECSDI]
    public class AbilityMetaAspect : EcsAspect
    {
        public ProtoPool<AbilityConfigurationReferenceComponent> ConfigurationReference;
        public ProtoPool<AbilityConfigurationComponent> Configuration;
        public ProtoPool<AbilityVisualComponent> Visual;
        public ProtoPool<NameComponent> Name;
        public ProtoPool<AbilityMetaComponent> Meta;
        public ProtoPool<AbilitySlotComponent> Slot;
        public ProtoPool<AbilityIdComponent> Id;
        public ProtoPool<AbilityBlockedComponent> Blocked;
        //arena specific
        public ProtoPool<AbilityCategoryComponent> Category;
        public ProtoPool<AbilityLevelComponent> Level;
        public ProtoPool<PassiveAbilityComponent> IsPassive;
    }
}