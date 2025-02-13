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
    /// System for removes states from entities.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveStateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameStatesAspect _stateAspect;
        private GameStatesMap _stateData;

        private ProtoIt _stateFilter = It
            .Chain<RemoveStateSelfRequest>()
            .Inc<StatesMapComponent>()
            .End();

        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var stateIdComponent = ref _stateAspect.State.Get(stateEntity);
                var stateId = stateIdComponent.Id;
                var state = _stateData.States.Remove(stateId);
            }
        }
    }
}