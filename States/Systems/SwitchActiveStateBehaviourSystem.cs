namespace Game.Ecs.State.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Events;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class SwitchActiveStateBehaviourSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameStateBehaviourAspect _behaviourAspect;
        private GameStatesAspect _stateAspect;
        
        private ProtoIt _updateFilter = It
            .Chain<StateBehavioursMapComponent>()
            .Inc<UpdateStateBehaviourRequest>()
            .Inc<StateBehaviourComponent>()
            .Inc<StateComponent>()
            .End();
        
        private ProtoIt _stateFilter = It
            .Chain<StateBehavioursMapComponent>()
            .Inc<StateBehaviourComponent>()
            .Inc<StateComponent>()
            .Inc<StateChangedSelfEvent>()
            .End();


        public void Run()
        {
            //update to new behaviour is requested
            foreach (var updateEntity in _updateFilter)
            {
                _behaviourAspect.UpdateEntityBehaviour(updateEntity,_world);
                _behaviourAspect.UpdateStateBehaviour.Del(updateEntity);
            }

            //update to new behaviour if event is triggered
            foreach (var updateEntity in _stateFilter)
            {
                _behaviourAspect.UpdateEntityBehaviour(updateEntity,_world);
            }
        }
    }
}