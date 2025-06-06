namespace UniGame.Ecs.Proto.ViewControl.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessHideViewRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ViewControlAspect _viewControlAspect;
        
        private ProtoIt _filter = It
            .Chain<HideViewRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _viewControlAspect.Hide.Get(entity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!_viewControlAspect.Data.Has(destinationEntity))
                    continue;

                ref var viewData = ref _viewControlAspect.Data.Get(destinationEntity);
                if (!viewData.Views.TryGetValue(request.View, out var viewPackedEntity))
                    continue;

                if (!viewPackedEntity.Unpack(_world, out var viewEntity))
                    continue;

                ref var viewInstance = ref _viewControlAspect.Instance.Get(viewEntity);
                viewInstance.Count--;

                if (viewInstance.Count > 0)
                    continue;

                viewData.Views.Remove(request.View);

                Object.Destroy(viewInstance.ViewInstance);
                _world.DelEntity(viewEntity);
            }
        }
    }
}