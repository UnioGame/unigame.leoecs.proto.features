namespace Game.Ecs.State
{
    using System;
    using Cysharp.Threading.Tasks;
    using Data;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [Serializable]
    public abstract class BaseStateFeature<TStateComponent> : BaseStateFeature
        where TStateComponent : struct, IStateComponent
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddSystem(new StopStateSystemT<TStateComponent>());
            ecsSystems.AddSystem(new SetStateSystemT<TStateComponent>());
            
            return UniTask.CompletedTask;
        }
    }

    [Serializable]
    public abstract class BaseStateFeature : EcsFeature
    {
        
    }
}