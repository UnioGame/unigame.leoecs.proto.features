namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Runtime.ObjectPool.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// disable current active view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DisableActiveGameViewSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ParentGameViewAspect _parentViewAspect;

        private ProtoIt _disableViewFilter = It
            .Chain<DisableActiveGameViewRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _disableViewFilter)
            {
                ref var disableRequest = ref _parentViewAspect.Disable.Get(entity);
                if (!disableRequest.Value.Unpack(_world, out var targetEntity))
                    continue;

                if (!_parentViewAspect.ActiveView.Has(targetEntity)) continue;

                ref var activeViewComponent = ref _parentViewAspect.ActiveView.Get(targetEntity);
                if (!activeViewComponent.Value.Unpack(_world, out var activeEntity))
                    continue;

                if (!_parentViewAspect.View.Has(activeEntity))
                    continue;

                ref var gameObjectComponent = ref _parentViewAspect.View.Get(activeEntity);
                gameObjectComponent.Value.Despawn();
            }
        }
    }
}