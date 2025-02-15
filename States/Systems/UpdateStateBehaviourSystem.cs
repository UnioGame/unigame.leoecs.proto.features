namespace Game.Ecs.State.Systems
{
    using System;
    using Aspects;
    using Components;
    using Core.Death.Components;
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
    public class UpdateStateBehaviourSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameStateBehaviourAspect _behaviourAspect;
        private GameStatesAspect _stateAspect;

        private ProtoItExc _stateFilter = It
            .Chain<StateBehaviourComponent>()
            .Exc<DisabledComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _stateFilter)
            {
                ref var stateBehaviour = ref _behaviourAspect.StateBehaviour.Get(entity);
                ref var stateComponent = ref _stateAspect.State.Get(entity);
                
                var behaviour = stateBehaviour.Value;
                
                if(behaviour == null) continue;
                
                var next = behaviour.Update(entity,_world);
                if (next <= 0 || next == stateComponent.Id) continue;
                
                ref var changeRequest = ref _stateAspect.ChangeState.GetOrAddComponent(entity);
                changeRequest.StateId = next;
            }
        }
    }
}