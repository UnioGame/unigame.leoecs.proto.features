namespace Game.Ecs.GameActions.Systems
{
    using System;
    using ButtonAction.Aspects;
    using ButtonAction.Components;
    using ButtonAction.Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
    public class UiButtonActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private GameActionAspect _gameActionAspect;
        private ButtonActionAspect _buttonActionAspect;
        
        private ProtoIt _filter = It
            .Chain<ButtonGameActionSelfRequest>()
            .Inc<ButtonGameActionComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var buttonActionComponent = ref _buttonActionAspect.Action.Get(entity);
                var id = buttonActionComponent.Id;

                ref var gameActionRequest = ref _gameActionAspect.ActionSelfRequest.GetOrAdd(entity);
                gameActionRequest.Id = id;
            }
        }
    }
}