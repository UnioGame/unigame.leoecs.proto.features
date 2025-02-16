namespace Game.Modules.SequenceActions.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Data;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;

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
        public ProtoWorld World;
        
        //sequence
        public ProtoPool<SequenceDataComponent> SequenceData;
        public ProtoPool<SequenceProgressComponent> SequenceProgress;
        public ProtoPool<SequenceCompleteComponent> Complete;
        public ProtoPool<LifeTimeComponent> LifeTime;
        
        //actions
        public ProtoPool<SequenceActionComponent> SequenceAction;
        public ProtoPool<SequenceActionProgressComponent> ActionProgress;
        public ProtoPool<SequenceActionResultComponent> Result;
        
        //requests
        public ProtoPool<StartSequenceByIdRequest> StartById;
        public ProtoPool<StartSequenceRequest> Start;
        
        public ProtoPool<StartSequenceActionSelfRequest> StartAction;
        
        //events
        public ProtoPool<SequenceCompleteSelfEvent> CompleteEvent;
        
    }
}