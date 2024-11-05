namespace Game.Ecs.ButtonAction.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
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
    public class ButtonActionAspect : EcsAspect
    {
        // Component that determines whether button is interactable.
        public ProtoPool<InteractableButtonComponent> Interactable;
        // Component that determines whether button is selected.
        public ProtoPool<SelectedButtonComponent> Selected;
        
        // Representing a button action(click) self request.
        public ProtoPool<ButtonActionSelfRequest> ActionRequest;
        
        // Represents an event that is triggered when a button is unselected.   
        public ProtoPool<ButtonUnselectedSelfEvent> UnselectedEvent;
        // Represents an event that is triggered when a button is selected.
        public ProtoPool<ButtonSelectedSelfEvent> SelectedEvent;
    }
}