namespace Game.Ecs.ButtonAction.Aspects
{
    using System;
    using System.Runtime.CompilerServices;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Modules.leoecs.proto.features.GameActions.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// Aspect of a button action in the game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class GameActionAspect : EcsAspect
    {
        // Component that determines whether button is interactable.
        public ProtoPool<GameActionsMapComponent> GameActionsMap;

        public ProtoPool<GameActionSelfRequest> ActionSelfRequest;
        
        public ProtoPool<GameActionRequest> ActionRequest;
        
        //events
        public ProtoPool<GameActionEvent> ActionEvent;
        
        //filters
        public ProtoIt GameActionsFilter = It
            .Chain<GameActionsMapComponent>()
            .End();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsGameActionEnabled(int actionId)
        {
            if(actionId < 0) return false;
            
            var filterResult = GameActionsFilter.First();
            if (!filterResult.Ok) return false;
            
            ref var mapComponent = ref GameActionsMap.Get(filterResult.Entity);
            if(mapComponent.Length >= actionId) return false;
            
            return mapComponent.Status[actionId];
        }
        
    }
}