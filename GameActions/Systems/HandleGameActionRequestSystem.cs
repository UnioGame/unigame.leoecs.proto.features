namespace Game.Ecs.GameActions.Systems
{
    using System;
    using ButtonAction.Aspects;
    using ButtonAction.Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    /// <summary>
    /// System for handling main button actions.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class HandleGameActionRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameActionAspect _aspect;
        
        private ProtoIt _selfFilter = It
            .Chain<GameActionSelfRequest>()
            .End();
        
        private ProtoIt _filter = It
            .Chain<GameActionRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _selfFilter)
            {
                ref var actionRequest = ref _aspect.ActionSelfRequest.Get(entity);
                if(!_aspect.IsGameActionEnabled(actionRequest.Id))continue;
                ref var actionEvent = ref _aspect.ActionEvent.Add(entity);
                actionEvent.Id = actionRequest.Id;
            }
            
            foreach (var entity in _filter)
            {
                ref var actionRequest = ref _aspect.ActionSelfRequest.Get(entity);
                if(!_aspect.IsGameActionEnabled(actionRequest.Id))continue;
                ref var actionEvent = ref _aspect.ActionEvent.NewEntity();
                actionEvent.Id = actionRequest.Id;
            }
        }
    }
}