namespace Game.Ecs.State.Systems
{
    using Leopotam.EcsProto;
    using System;
    using System.Linq;
    using Aspects;
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
    public class InitializeStateSystem : IProtoInitSystem
    {
        private ProtoWorld _world;
        private StatesMap _statesMap;
        
        private StatesAspect _stateAspect;
        
        public void Init(IProtoSystems systems)
        {
            _stateAspect.StatesMap = _statesMap;
            _stateAspect.TypeStates = _statesMap.states.ToDictionary(x => (Type)x.stateType);
            _stateAspect.IntStates = _statesMap.states.ToDictionary(x => x.id);
            _stateAspect.NameStates = _statesMap.states.ToDictionary(x => x.name);
        }
        
    }
}