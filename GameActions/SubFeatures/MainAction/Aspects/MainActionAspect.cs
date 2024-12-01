namespace Game.Ecs.ButtonAction.SubFeatures.MainAction.Aspects
{
    using System;
    using Components;
    using Components.Events;
    using Data;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    /// <summary>
    /// Aspect for the Main Action sub feature of the ButtonAction feature. 
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MainActionAspect : EcsAspect
    {
        // Components
        public ProtoPool<ButtonActionComponent<GameActionId>> Action;
        public ProtoPool<ButtonActionEvent<GameActionId>> ActionEvent;
    }
}