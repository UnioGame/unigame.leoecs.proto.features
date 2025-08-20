namespace Game.Ecs.State.Systems
{
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
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
    public class StopStateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private StatesAspect _stateAspect;

        private ProtoIt _stateFilter = It
            .Chain<StopStateSelfRequest>()
            .Inc<StateComponent>()
            .End();

        public void Run()
        {
            foreach (var stateEntity in _stateFilter)
            {
                ref var setStateRequest = ref _stateAspect.SetSelfState.GetOrAddComponent(stateEntity);
                setStateRequest.Id = 0;
            }
        }
    }
}