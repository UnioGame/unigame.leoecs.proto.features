namespace Game.Ecs.Input.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class InputAspect : EcsAspect
    {
        //filters
        public ProtoIt SwitchInputFilter = It
            .Chain<SwitchInputMapRequest>()
            .End();

        public ProtoIt InputFilter = It
            .Chain<InputActionsComponent>()
            .End();
        
        // Component representing an input actions.
        public ProtoPool<InputActionsComponent> InputActions;
        public ProtoPool<UserInputTargetComponent> InputTarget;
            
        // Request component used to signal the system to switch to a different input map.
        public ProtoPool<SwitchInputMapRequest> SwitchInputMap;
    }
}