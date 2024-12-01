namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DisableGameViewOnActivateSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ParentGameViewAspect _viewAspect;

        private ProtoIt _activateFilter = It
            .Chain<ActivateGameViewRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _activateFilter)
            {
                ref var activateRequest = ref _viewAspect.Activate.Get(requestEntity);
                //disable active view
                var disableEntity = _world.NewEntity();
                ref var disableRequest = ref _viewAspect.Disable.Add(disableEntity);
                disableRequest.Value = activateRequest.Source;
            }
        }
    }
}