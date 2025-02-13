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

    /// <summary>
    /// System for adding a state to entities.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class AddStateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameStatesAspect _stateAspect;
        
        private GameStatesMap _statesData;

        private ProtoItExc _stateFilter = It
            .Chain<AddStateSelfRequest>()
            .Inc<StatesMapComponent>()
            .Exc<StateComponent>()
            .End();

        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var addStateSelfRequest = ref _stateAspect.AddState.Get(stateEntity);
                ref var statesMapComponent = ref _stateAspect.StatesMap.Get(stateEntity);
                
                var newStateId = addStateSelfRequest.State;
                
                if(!_statesData.States.ContainsKey(newStateId)) continue;
                statesMapComponent.States.Add(newStateId);
            }
        }
    }
}