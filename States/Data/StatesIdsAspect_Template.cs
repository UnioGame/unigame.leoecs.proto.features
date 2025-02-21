namespace Game.Modules.States.Data
{
    using System;
    using System.Collections.Generic;
    using Ecs.State.Aspects;
    using Ecs.State.Components.Events;
    using Ecs.State.Data;
    using Ecs.State.Systems;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    [ECSDI]
    public class StatesIdsAspect_Template : EcsAspect
    {
        public ProtoWorld World;
        
        public Dictionary<int,Type> StateTypes = new(32)
        {
            //BEGIN_MAP
        };

        //BEGIN_POOLS
        
        public void AddStateComponent(ProtoEntity entity, int state)
        {
            if (!StateTypes.TryGetValue(state, out var stateType)) return;
            World.GetOrAddComponent(entity, stateType);
        }
        
        public void RemoveStateComponent(ProtoEntity entity, int state)
        {
            if (!StateTypes.TryGetValue(state, out var stateType)) return;
            World.TryRemoveComponent(entity, stateType);
        }
    }

    [Serializable]
    [ECSDI]
    public class StatesIds_System : IEcsRunSystem
    {
        private GameStatesAspect _statesAspect;
        private StatesIdsAspect_Template _statesIdsAspect;

        private ProtoIt _filter = It
            .Chain<StateChangedSelfEvent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var eventComponent = ref _statesAspect.StateChanged.Get(entity);
                _statesIdsAspect.AddStateComponent(entity, eventComponent.NewId);
                _statesIdsAspect.RemoveStateComponent(entity, eventComponent.FromStateId);
            }
        }
    }
    
}