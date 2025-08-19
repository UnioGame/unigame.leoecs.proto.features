namespace Game.ECS.UI.GameActionsViews.Systems
{
    using System;
    using Data;
    using global::Game.Ecs.ButtonAction.Aspects;
    using global::Game.Ecs.ButtonAction.Components.Events;
    using global::Game.Ecs.GameActions.Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.ViewSystem.Aspects;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class OpenViewByGameActionSystem : IProtoRunSystem
    {
        private GameActionViewsData _actionViewData;
        private ProtoWorld _world;

        private ViewAspect _viewAspect;
        private ViewContainerAspect _viewContainerAspect;
        private GameActionAspect _gameActionAspect;
        
        private ProtoIt _gameActionEventFilter = It
            .Chain<GameActionEvent>()
            .End();

        public void Run()
        {
            foreach (var entity in _gameActionEventFilter)
            {
                ref var eventComponent = ref _gameActionAspect.ActionEvent.Get(entity);

                var id = eventComponent.Id;
                var gameId = (GameActionId)id;

                foreach (var viewItem in _actionViewData.gameActionsViews)
                {
                    if(viewItem.gameActionId != gameId) continue;
                    
                    //todo: Сергей посмотри этот код, тут не существующий метод
                    // if (viewItem.spawnInContainer)
                    // {
                    //     _viewAspect.ShowView(viewItem.view, viewItem.useBusyContainer);
                    // }
                    // else
                    // {
                    //     _viewAspect.ShowView(viewItem.view, viewItem.viewType);
                    // }
                }
            }
        }
    }
}