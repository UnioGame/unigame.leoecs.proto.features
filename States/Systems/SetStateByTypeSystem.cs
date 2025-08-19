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
    public class SetStateByTypeSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private StatesMap _statesMap;
        
        private StatesAspect _stateAspect;

        private ProtoIt _stateFilter = It
            .Chain<SetStateByTypeSelfRequest>()
            .Inc<StateComponent>()
            .End();
        
        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var typeSelfRequest = ref _stateAspect.SetByTypeSelfState.Get(stateEntity);
                var targetType = typeSelfRequest.Id;
                if(!_stateAspect.TypeStates.TryGetValue(targetType,out var targetState))
                    continue;
                
                ref var request = ref _stateAspect.SetSelfState.Add(stateEntity);
                request.Id = targetState.id;
            }
        }

    }
}