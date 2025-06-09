namespace UniGame.Ecs.Proto.Characteristics.Base.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;

    /// <summary>
    /// modifications of characteristics values
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ModificationsAspect : EcsAspect
    {
        public ProtoPool<BaseModificationsValueComponent> BaseModificationValue;
        public ProtoPool<PercentModificationsValueComponent> PercentModificationValue;
        public ProtoPool<MaxLimitModificationsValueComponent> MaxLimitModificationValue;
        
        public ProtoPool<CharacteristicLinkComponent> CharacteristicLink;
        public ProtoPool<ModificationSourceTrackComponent> ModificationSourceTrack;
        public ProtoPool<ModificationSourceLinkComponent> SourceLink;
        public ProtoPool<ModificationSourceComponent> ModificationSource;
        public ProtoPool<ModificationPercentComponent> ModificationPercent;
        public ProtoPool<ModificationMaxLimitComponent> ModificationMaxLimit;
        public ProtoPool<ModificationComponent> Modification;
        
        //requests
        public ProtoPool<AddModificationRequest> AddModification;
        public ProtoPool<CreateModificationRequest> CreateModification;
        public ProtoPool<ResetModificationsRequest> ResetModifications;
    }
}