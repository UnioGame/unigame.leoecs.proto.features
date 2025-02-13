namespace Game.Ecs.State.Systems
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using Data;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// System for changing the state of an entity.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ChangeStateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameStatesAspect _stateAspect;
        
        private GameStatesMap _statesData;

        private ProtoIt _stateFilter = It
            .Chain<ChangeStateSelfRequest>()
            .Inc<StateComponent>()
            .Inc<StatesMapComponent>()
            .End();

        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var stateIdComponent = ref _stateAspect.State.Get(stateEntity);
                ref var statesMapComponent = ref _stateAspect.StatesMap.Get(stateEntity);
                ref var changeState = ref _stateAspect.ChangeState.Get(stateEntity);
                
                var newStateId = changeState.StateId;
                var activeStateId = stateIdComponent.Id;
                
                if(!statesMapComponent.States.Contains(newStateId))
                    continue;
                
                var currentStateId = stateIdComponent.Id;
                if(currentStateId == changeState.StateId)
                    continue;

                stateIdComponent.Id = newStateId;

                ref var stateChangedEvent = ref _stateAspect.StateChanged.Add(stateEntity);
                stateChangedEvent.NewId = newStateId;
                stateChangedEvent.FromStateId = activeStateId;
            }
        }
    }
}