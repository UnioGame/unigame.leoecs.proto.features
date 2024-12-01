namespace Game.Ecs.Input.Mouse.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine.InputSystem;

    /// <summary>
    /// System for mouse position.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class GetMousePositionSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private MouseAspect _mouseAspect;

        public void Run()
        {
            foreach (var mouseEntity in _mouseAspect.MouseInputFilter)
            {
                if (Mouse.current == null) continue; 
                
                var mousePosition = Mouse.current.position.ReadValue();
                
                ref var mouseInputComponent = ref _mouseAspect.MouseInput.Get(mouseEntity);
                mouseInputComponent.MousePosition = mousePosition;
            }
        }
    }
}