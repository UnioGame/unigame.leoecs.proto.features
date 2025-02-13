namespace Game.Ecs.State.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Events;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class UpdateStateHistorySystem : IProtoInitSystem, IProtoRunSystem
    {
        private GameStatesAspect _stateAspect;
        private ProtoWorld _world;
        
        private ProtoIt _stateFilter = It
            .Chain<StateChangedSelfEvent>()
            .Inc<StateHistoryComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var entity in _stateFilter)
            {
                ref var stateChangedEvent = ref _stateAspect.StateChanged.Get(entity);
                ref var stateHistoryComponent = ref _stateAspect.StatesHistory.Get(entity);
                stateHistoryComponent.States.Add(stateChangedEvent.NewId);
            }
        }
    }
}