namespace Game.Ecs.State.Aspects
{
    using Leopotam.EcsProto;
    using System;
    using System.Collections.Generic;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Converters;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Collections;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class GameStatesAspect : EcsAspect
    {

        public ProtoWorld World;
        
        /// <summary>
        /// Active state of entity
        /// </summary>
        public ProtoPool<StateComponent> State;
        
        /// <summary>
        /// optional history storage of states
        /// </summary>
        public ProtoPool<StateHistoryComponent> StatesHistory;
        
        /// <summary>
        /// possible states for entity
        /// </summary>
        public ProtoPool<StatesMapComponent> StatesMap;

        //REQUESTS
        
        /// <summary>
        /// Struct representing a request to add a state to the current entity.
        /// </summary>
        public ProtoPool<AddStateSelfRequest> AddState;
        /// <summary>
        /// Request to change the state of an entity.
        /// </summary>
        public ProtoPool<ChangeStateSelfRequest> ChangeState;
        /// <summary>
        /// Request to remove the state from entity.
        /// </summary>
        public ProtoPool<RemoveStateSelfRequest> RemoveState;
        
        //EVENTS
        
        /// <summary>
        /// Event indicating a state change.
        /// </summary>
        public ProtoPool<StateChangedSelfEvent> StateChanged;


        public static void AddStatesBehaviours(ProtoEntity entity, ProtoWorld world,List<StateBehaviourData> behaviours)
        {
            var behaviourEntity = GameStateBehaviourAspect.CreateStateBehaviourEntity(entity, world);
            ref var behavioursComponent = ref world.GetOrAddComponent<StateBehavioursMapComponent>(behaviourEntity);
            
            foreach (var behaviour in behaviours)
            {
                behavioursComponent.Behaviours.Add(behaviour.stateId, behaviour.stateBehaviour);
            }
        }
        
        public static ProtoEntity CreateStatesEntity(ProtoEntity entity,ProtoWorld world, bool useHistory = false)
        {
            ref var statesMapComponent = ref world.AddComponent<StatesMapComponent>(entity);
            ref var stateComponent = ref world.AddComponent<StateComponent>(entity);

            if (useHistory)
            {
                ref var historyComponent = ref world.AddComponent<StateHistoryComponent>(entity);
                historyComponent.States = new NativeList<int>(0, Allocator.Persistent);
            }
            
            statesMapComponent.States = new NativeHashSet<int>(0, Allocator.Persistent);

            return entity;
        }

    }
}