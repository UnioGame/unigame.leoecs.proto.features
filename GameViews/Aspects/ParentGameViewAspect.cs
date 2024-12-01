namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class ParentGameViewAspect : EcsAspect
    {
        public ProtoPool<GameViewParentComponent> Parent;
        public ProtoPool<ActiveGameViewComponent> ActiveView;
        public ProtoPool<GameObjectComponent> View;
        
        //requests
        public ProtoPool<ActivateGameViewRequest> Activate;
        public ProtoPool<DisableActiveGameViewRequest> Disable;
    }
}