namespace Game.Ecs.Input.Mouse.Systems
{
    using UniRx;
    using System;
    using Aspects;
    using Components;
    using Input.Aspects;
    using Input.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Core.Runtime;
    using UnityEngine.InputSystem;
    using UniModules.UniCore.Runtime.DataFlow;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// System responsible for detecting mouse input and sending corresponding requests.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MouseInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        private MouseAspect _mouseAspect;
        private InputAspect _inputAspect;
        private ILifeTime _worldLifetime;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _worldLifetime = _world.GetWorldLifeTime();
        }

        public void Run()
        {
            foreach (var inputEntity in _mouseAspect.InputActionFilter)
            {
                ref var inputActionComponent = ref _inputAspect.InputActions.Get(inputEntity);
                
                var inputActions = inputActionComponent.InputActions;
                var inputAction = inputActions.UI.Click;
                
                var observable = Observable.FromEvent<InputAction.CallbackContext>(
                    handler => inputAction.performed += handler,
                    handler => inputAction.performed -= handler
                );

                observable.Subscribe(LeftClick).AddTo(_worldLifetime);
                inputAction.Enable();

                _mouseAspect.MouseInput.Add(inputEntity);
            }
        }

        private void LeftClick(InputAction.CallbackContext context)
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var clickValue = context.ReadValue<float>();
            var actionName = context.action.name;
            
            if ((int)clickValue == 1)
                return;
            
            var eventEntity = _world.NewEntity();

            ref var clickLeftMouseButton = ref _mouseAspect.ClickLeftMouseButtonRequest.Add(eventEntity);
            clickLeftMouseButton.MousePosition = mousePosition;
            clickLeftMouseButton.ActionName = actionName;
        }
    }
}