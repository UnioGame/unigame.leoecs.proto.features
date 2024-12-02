namespace Game.Ecs.ButtonAction.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
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
        
        // Components
        public ProtoPool<ButtonGameActionComponent> Action;
        // Representing a button action(click) self request.
        public ProtoPool<ButtonGameActionSelfRequest> ActionRequest;
        
        // Represents an event that is triggered when a button is unselected.   
        public ProtoPool<ButtonUnselectedSelfEvent> UnselectedEvent;
        // Represents an event that is triggered when a button is selected.
        public ProtoPool<ButtonSelectedSelfEvent> SelectedEvent;
        
        public ProtoIt InteractableFilter = It
            .Chain<InteractableButtonComponent>()
            .Inc<ButtonGameActionComponent>()
            .End();
    }
}