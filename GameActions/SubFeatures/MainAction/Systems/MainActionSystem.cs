namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Systems
{
    using System;
    using Aspects;
    using ButtonAction.Aspects;
    using Components;
    using Components.Requests;
    using Data;
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
    public class MainActionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private MainActionAspect _mainActionAspect;
        private ButtonActionAspect _buttonActionAspect;
        
        private ProtoIt _filter = It
            .Chain<ButtonActionSelfRequest>()
            .Inc<ButtonActionComponent<GameActionId>>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var buttonActionComponent = ref _mainActionAspect.Action.Get(entity);
                var id = buttonActionComponent.Id;
                
                ref var buttonActionEvent = ref _mainActionAspect.ActionEvent.Add(_world.NewEntity());
                buttonActionEvent.Id = id;
                
                _buttonActionAspect.ActionRequest.TryRemove(entity);
            }
        }
    }
}