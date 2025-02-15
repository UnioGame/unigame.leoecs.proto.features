namespace Game.Modules.SequenceActions.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Data;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using SequenceActionResultComponent = Data.SequenceActionResultComponent;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SequenceActionAspect : EcsAspect
    {
        public ProtoPool<SequenceActionComponent> SequenceAction;
        public ProtoPool<SequenceActionProgressComponent> Progress;
        public ProtoPool<SequenceActionActiveComponent> Active;
        
        public ProtoPool<SequenceActionResultComponent> Result;
        
        //requests
        
        public ProtoPool<StartSequenceActionRequest> Start;
        public ProtoPool<StartSequenceActionByIdRequest> StartById;
        
    }
}