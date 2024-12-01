namespace UniGame.Ecs.Proto.Gameplay.LevelProgress.Systems
{
    using System;
    using Aspects;
    using Components;
    using GameResources.Aspects;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// activate new view
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ActivateGameViewSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private ParentGameViewAspect _parentViewAspect;
        private GameViewAspect _viewAspect;
        private GameResourceAspect _gameResourceAspect;

        private ProtoIt _activateFilter = It
            .Chain<ActivateGameViewRequest>()
            .End();

        public void Run()
        {
            foreach (var requestEntity in _activateFilter)
            {
                ref var activateRequest = ref _parentViewAspect.Activate.Get(requestEntity);
 
                if(!activateRequest.Source.Unpack(_world,out var target)) continue;
                if(!_parentViewAspect.Parent.Has(target)) continue;
                
                ref var viewParentComponent = ref _parentViewAspect.Parent.Get(target);
                    
                var gameResourceEntity = _world.NewEntity();
                ref var gameResourceRequest = ref _gameResourceAspect.SpawnRequest.Add(gameResourceEntity);

                var viewEntity = _world.NewEntity();
                _viewAspect.View.Add(viewEntity);
                var viewPacked = viewEntity.PackEntity(_world);
                
                gameResourceRequest.ResourceId = activateRequest.View;
                gameResourceRequest.Parent = viewParentComponent.Parent;
                gameResourceRequest.LocationData.Rotation = viewParentComponent.Rotation;
                gameResourceRequest.LocationData.Position = viewParentComponent.Position;
                gameResourceRequest.LocationData.Scale = viewParentComponent.Scale;

                ref var activeViewComponent = ref _parentViewAspect.ActiveView.GetOrAddComponent(target);
                activeViewComponent.Value = viewPacked;
            }
        }
    }
}