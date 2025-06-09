namespace Game.Modules.SequenceActions.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Data;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap;
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
    public class SequenceAspect : EcsAspect
    {
        public ProtoWorld World;
        
        //sequence
        public ProtoPool<SequenceComponent> Sequence;
        public ProtoPool<SequenceCompleteComponent> Complete;
        public ProtoPool<SequenceFailedComponent> Failed;
        /// <summary>
        /// sequence result data updated every frame
        /// </summary>
        public ProtoPool<SequenceResultComponent> Result;
        /// <summary>
        /// sequence life time
        /// </summary>
        public ProtoPool<LifeTimeComponent> LifeTime;
        /// <summary>
        /// mark entity for auto destroy after sequence complete
        /// </summary>
        public ProtoPool<SequenceAutoDestroyComponent> AutoDestroy;
        
        /// <summary>
        /// merk entity as active sequence
        /// </summary>
        public ProtoPool<SequenceActiveComponent> Active;
        
        /// <summary>
        /// sequence action data
        /// </summary>
        public ProtoPool<SequenceActionComponent> SequenceAction;
        
        
        //requests
        public ProtoPool<StartSequenceByIdRequest> StartById;
        public ProtoPool<StartSequenceRequest> Start;
        
        //events
        public ProtoPool<SequenceCompleteSelfEvent> CompleteEvent;
        
    }
}