namespace Game.Ecs.Input.Mouse.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Input.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// Aspect containing a pool for components related to mouse input.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MouseAspect : EcsAspect
    {
        //filters
        public ProtoIt LeftMouseFilter= It
            .Chain<ClickLeftMouseButtonRequest>()
            .End();
        
        public ProtoIt MouseInputFilter = It
            .Chain<MouseInputComponent>()
            .End();
        
        public ProtoItExc InputActionFilter =  It
            .Chain<InputActionsComponent>()
            .Exc<MouseInputComponent>()
            .End();
        
        // Component indicating subscribed to mouse input events.
        public ProtoPool<MouseInputComponent> MouseInput;

        // Request component used to signal the system to process a left mouse button click.
        public ProtoPool<ClickLeftMouseButtonRequest> ClickLeftMouseButtonRequest;
        
        /// Event component used to signal the system to process a left mouse button click.
        public ProtoPool<ClickLeftMouseButtonEvent> ClickLeftMouseButtonEvent;
    }
}