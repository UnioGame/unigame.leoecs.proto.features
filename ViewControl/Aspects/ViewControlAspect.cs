namespace UniGame.Ecs.Proto.ViewControl.Aspects
{
    using System;
    using Components;
    using Components.Requests;
    using Leopotam.EcsProto;
    using LeoEcs.Bootstrap;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ViewControlAspect : EcsAspect
    {
        public ProtoPool<ViewDataComponent> Data;
        public ProtoPool<ViewInstanceComponent> Instance;
        
        public ProtoPool<HideViewRequest> Hide;
        public ProtoPool<ShowViewRequest> Show;
    }
}