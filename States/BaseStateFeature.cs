namespace Game.Ecs.State
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [Serializable]
    public abstract class BaseStateFeature<TState,TStateFinishedEvent> : BaseStateFeature
        where TState : struct, IStateComponent
        where TStateFinishedEvent : struct
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //remote custom state changed event
            ecsSystems.DelHere<TStateFinishedEvent>();
            
            ecsSystems.AddSystem(new SetStateSystemT<TState,TStateFinishedEvent>());
            
            return UniTask.CompletedTask;
        }
    }

    [Serializable]
    public abstract class BaseStateFeature : EcsFeature
    {
        
    }
}