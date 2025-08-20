namespace Game.Ecs.State
{
    using System;
    using Cysharp.Threading.Tasks;
    using State;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [Serializable]
    public abstract class BaseStateFeature<TState,TStateFinishedEvent,TStateStartEvent> : BaseStateFeature
        where TState : struct, IStateComponent
        where TStateFinishedEvent : struct
        where TStateStartEvent : struct
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register custom state
            ecsSystems.RegisterState<TState,TStateFinishedEvent,TStateStartEvent>();
            
            return UniTask.CompletedTask;
        }
    }

    [Serializable]
    public abstract class BaseStateFeature : EcsFeature,IStateFeature
    {
        
    }
}