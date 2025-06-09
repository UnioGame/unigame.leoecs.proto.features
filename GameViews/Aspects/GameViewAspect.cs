namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Components;
    using LeoEcs.Bootstrap;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class GameViewAspect : EcsAspect
    {
        public ProtoPool<GameViewComponent> View;
        public ProtoPool<GameObjectComponent> GameObject;
    }
}