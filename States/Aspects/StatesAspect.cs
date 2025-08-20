namespace Game.Ecs.State.Aspects
{
    using Leopotam.EcsProto;
    using System;
    using System.Collections.Generic;
    using Components;
    using Components.Events;
    using Components.Requests;
    using State;
    using UniGame.LeoEcs.Bootstrap;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class StatesAspect : EcsAspect
    {
        public ProtoWorld World;
        
        public Dictionary<Type, StateData> TypeStates = new();
        public Dictionary<int, StateData> IntStates = new();
        public Dictionary<string, StateData> NameStates = new();
        public StatesMap StatesMap;
        
        /// <summary>
        /// Active state of entity
        /// </summary>
        public ProtoPool<StateComponent> State;

        //REQUESTS
        /// <summary>
        /// Request to change the state of an entity.
        /// </summary>
        public ProtoPool<SetStateSelfRequest> SetSelfState;
        
        /// <summary>
        /// Request to remove the state from entity.
        /// </summary>
        public ProtoPool<StopStateSelfRequest> StopSelfState;
        
        //EVENTS
        
        /// <summary>
        /// Event indicating a state change.
        /// </summary>
        public ProtoPool<StateChangedSelfEvent> StateSelfChanged;

        //tools
        public int GetStateId(Type stateType)
        {
            TypeStates.TryGetValue(stateType, out var stateData);
            return stateData?.id ?? 0;
        }

    }
}