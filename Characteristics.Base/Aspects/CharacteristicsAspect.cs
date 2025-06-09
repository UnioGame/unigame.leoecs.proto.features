namespace UniGame.Ecs.Proto.Characteristics.Base.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// data of characteristics components
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class CharacteristicsAspect : EcsAspect
    {
        //aspects
        public ModificationsAspect Modifications;
        
        
        public ProtoPool<CharacteristicBaseValueComponent> BaseValue;
        public ProtoPool<CharacteristicChangedComponent> Changed;
        public ProtoPool<CharacteristicComponent> Characteristic;
        public ProtoPool<CharacteristicDefaultValueComponent> DefaultValue;
        public ProtoPool<CharacteristicValueComponent> Value;
        public ProtoPool<CharacteristicLinkComponent> CharacteristicLink;
        public ProtoPool<CharacteristicPreviousValueComponent> PreviousValue;
        public ProtoPool<MinValueComponent> MinValue;
        public ProtoPool<MaxValueComponent> MaxValue;
        public ProtoPool<PercentModificationsValueComponent> PercentValue;
        public ProtoPool<MaxLimitModificationsValueComponent> MaxLimitValue;
        
        //requests
        public ProtoPool<ResetCharacteristicMaxLimitSelfRequest> ResetMaxLimit;
        public ProtoPool<ChangeCharacteristicBaseRequest> ChangeBaseValue;
        public ProtoPool<ChangeCharacteristicRequest> ChangeValue;
        public ProtoPool<ChangeMaxLimitRequest> ChangeMaxLimit;
        public ProtoPool<ChangeMinLimitRequest> ChangeMinLimit;
        public ProtoPool<RecalculateCharacteristicSelfRequest> Recalculate;
        public ProtoPool<ResetCharacteristicRequest> Reset;
        public ProtoPool<RecalculateModificationSelfRequest> RecalculateModifications;
        public ProtoPool<RemoveModificationRequest> RemoveModification;
        
        //events
        public ProtoPool<ResetCharacteristicsEvent> OnCharacteristicsReset;
        public ProtoPool<CharacteristicValueChangedEvent> OnValueChanged;
    }
}