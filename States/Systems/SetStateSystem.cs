namespace Game.Ecs.State.Systems
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using State;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public class SetStateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private StatesMap _statesMap;
        
        private StatesAspect _stateAspect;

        private ProtoIt _stateFilter = It
            .Chain<SetStateSelfRequest>()
            .Inc<StateComponent>()
            .End();
        
        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var stateComponent = ref _stateAspect.State.Get(stateEntity);
                ref var changeState = ref _stateAspect.SetSelfState.Get(stateEntity);
                
                var newStateId = changeState.Id;
                var activeStateId = stateComponent.Value;
                
                var currentStateId = stateComponent.Value;
                if(currentStateId == newStateId) continue;

                stateComponent.Value = newStateId;

                ref var stateChangedEvent = ref _stateAspect.StateSelfChanged.Add(stateEntity);
                stateChangedEvent.NewId = newStateId;
                stateChangedEvent.FromStateId = activeStateId;
            }
        }

    }
}