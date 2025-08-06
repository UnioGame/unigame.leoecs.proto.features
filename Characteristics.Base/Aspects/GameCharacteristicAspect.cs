namespace UniGame.Ecs.Proto.Characteristics.Base.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Base;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// new characteristic aspect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public abstract class GameCharacteristicAspect<TCharacteristic> : EcsAspect 
        where TCharacteristic : struct
    {
        //components
        public ProtoPool<TCharacteristic> Characteristic;
        public ProtoPool<CharacteristicLinkComponent<TCharacteristic>> CharacteristicLink;
        public ProtoPool<CharacteristicComponent<TCharacteristic>> Value;
        public ProtoPool<CharacteristicChangedComponent<TCharacteristic>> Changed;
        public ProtoPool<CharacteristicOwnerComponent<TCharacteristic>> CharacteristicOwner;
        
        //requests
        public ProtoPool<CreateCharacteristicRequest<TCharacteristic>> Create;
        public ProtoPool<ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>> Reset;
        public ProtoPool<ChangeMinLimitSelfRequest<TCharacteristic>> ChangeMinLimit;
        public ProtoPool<ChangeCharacteristicValueRequest<TCharacteristic>> ChangeValue;
        public ProtoPool<ChangeMaxLimitSelfRequest<TCharacteristic>> ChangeMaxLimit;
        public ProtoPool<ChangeCharacteristicBaseRequest<TCharacteristic>> ChangeBaseValue;
        public ProtoPool<ResetCharacteristicSelfRequest<TCharacteristic>> ResetValue;
        public ProtoPool<ResetCharacteristicModificationsSelfRequest<TCharacteristic>> ResetModifications;
        public ProtoPool<RecalculateCharacteristicSelfRequest<TCharacteristic>> Recalculate;
        //events
        public ProtoPool<CharacteristicValueChangedEvent<TCharacteristic>> OnValueChanged;
        public ProtoPool<OwnerCharacteristicChangedSelfEvent<TCharacteristic>> OnOwnerCharacteristicChanged;
        public ProtoPool<ResetCharacteristicsEvent> OnCharacteristicsReset;
        
        //modifications requests
        public ProtoPool<AddModificationRequest<TCharacteristic>> AddModification;
        public ProtoPool<RemoveCharacteristicModificationRequest<TCharacteristic>> RemoveModification;
        public ProtoPool<ResetCharacteristicModificationsSelfRequest<TCharacteristic>> RemoveSelfModifications;
        public ProtoPool<CreateModificationRequest<TCharacteristic>> CreateModification;
    }
}