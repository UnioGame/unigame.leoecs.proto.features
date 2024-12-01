namespace Game.Ecs.Input.Mouse.Systems
{
    using System;
    using Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// A system responsible for catching mouse input events and generating corresponding events.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CatchMouseInputSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private MouseAspect _mouseAspect;
        
        public void Run()
        {
            foreach (var requestEntity in _mouseAspect.LeftMouseFilter)
            {
                ref var clickLeftMouseButtonRequest = ref _mouseAspect.ClickLeftMouseButtonRequest.Get(requestEntity);

                var mousePosition = clickLeftMouseButtonRequest.MousePosition;
                var actionName = clickLeftMouseButtonRequest.ActionName;

                var eventEntity = _world.NewEntity();
                ref var clickLeftMouseButtonEvent = ref _mouseAspect.ClickLeftMouseButtonEvent.Add(eventEntity);

                clickLeftMouseButtonEvent.MousePosition = mousePosition;
                clickLeftMouseButtonEvent.ActionName = actionName;
            }
        }
    }
}