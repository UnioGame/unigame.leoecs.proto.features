namespace Vampire.Game.ECS.UI.GameActionsViews.Systems
{
    using System;
    using global::Game.Ecs.ButtonAction.Aspects;
    using global::Game.Ecs.ButtonAction.Components.Events;
    using global::Game.Ecs.GameActions.Data;
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
    public class TriggerComponentByGameActionSystem<TComponent> : IProtoRunSystem where TComponent : struct
    {
        private int id;
        
        private ProtoWorld _world;
        private GameActionAspect _gameActionAspect;
        private ProtoPool<TComponent> _componentPool;
        
        private ProtoIt _gameActionEventFilter = It
            .Chain<GameActionEvent>()
            .End();

        public TriggerComponentByGameActionSystem(GameActionId actionId)
        {
            id = actionId;
        }
        
        public TriggerComponentByGameActionSystem(int actionId)
        {
            id = actionId;
        }

        public void Run()
        {
            foreach (var entity in _gameActionEventFilter)
            {
                ref var eventComponent = ref _gameActionAspect.ActionEvent.Get(entity);
                if(eventComponent.Id != id) continue;
                _componentPool.GetOrAdd(entity);
            }
        }
    }
}