namespace UniGame.Ecs.Proto.ViewControl.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    [Serializable]
    [ECSDI]
    public sealed class ProcessShowViewRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ViewControlAspect _viewControlAspect;

        private ProtoIt _filter = It
            .Chain<ShowViewRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _viewControlAspect.Show.Get(entity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                ref var viewData = ref _viewControlAspect.Data.GetOrAddComponent(destinationEntity);
                if (viewData.Views.TryGetValue(request.View, out var viewPackedEntity))
                {
                    if (viewPackedEntity.Unpack(_world, out var viewEntity))
                    {
                        ref var viewInstance = ref _viewControlAspect.Instance.Get(viewEntity);
                        viewInstance.Count++;
                    }

                    continue;
                }

                var instance = Object.Instantiate(request.View, request.Root);
                instance.transform.localPosition = Vector3.zero;
                var localScale = request.Root.localScale;
                instance.transform.localScale = new Vector3(request.Size.x / localScale.x,
                    request.Size.y / localScale.y, request.Size.z / localScale.z);

                var newViewEntity = _world.NewEntity();
                ref var newViewInstance = ref _viewControlAspect.Instance.Add(newViewEntity);
                newViewInstance.Count++;
                newViewInstance.ViewInstance = instance;

                viewData.Views.Add(request.View, _world.PackEntity(newViewEntity));

                if (!request.Destination.Unpack(_world, out var unpackedDestination))
                {
                    GameLog.LogError("ProcessShowViewRequestSystem: Cannot unpack destination entity");
                    continue;
                }

                unpackedDestination.AddChild(newViewEntity, _world);
            }
        }
    }
}