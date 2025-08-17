namespace Game.Ecs.State.Systems
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using System;
    using Aspects;
    using Components;
    using Components.Events;
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
    public class SetStateSystemT<TStateComponent> : IProtoRunSystem
        where TStateComponent : struct, IStateComponent
    {
        private ProtoWorld _world;
        private StatesAspect _stateAspect;

        private ProtoIt _stateFilter = It
            .Chain<StateChangedSelfEvent>()
            .Inc<StateComponent>()
            .End();
        
        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                var stateId = StatesAspectT<TStateComponent>.StateId;

                ref var changedSelfEvent = ref _stateAspect.StateSelfChanged.Get(stateEntity);
                if (changedSelfEvent.FromStateId == stateId)
                    _world.TryRemoveComponent<TStateComponent>(stateEntity);

                if(stateId != changedSelfEvent.NewId) continue;

                ref var stateTComponent = ref _world
                    .GetOrAddComponent<TStateComponent>(stateEntity);
            }
        }
    }
}