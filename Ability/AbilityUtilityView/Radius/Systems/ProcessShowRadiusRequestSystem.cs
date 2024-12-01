namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Aspects;
    using Component;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using ViewControl.Aspects;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessShowRadiusRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        private ViewControlAspect _viewControlAspect;

        private ProtoIt _filter = It
            .Chain<ShowRadiusRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _abilityUtilityViewAspect.ShowRadius.Get(entity);
                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var state = ref _abilityUtilityViewAspect.RadiusViewState.GetOrAddComponent(sourceEntity);
                if (state.RadiusViews.TryGetValue(request.Destination, out var view))
                {
                    if (request.Radius == view)
                        continue;

                    var hideRequestEntity = _world.NewEntity();
                    ref var hideViewRequest = ref _viewControlAspect.Hide.Add(hideRequestEntity);

                    hideViewRequest.View = view;
                    hideViewRequest.Destination = request.Destination;
                }

                state.RadiusViews[request.Destination] = request.Radius;

                var showRequestEntity = _world.NewEntity();
                ref var showViewRequest = ref _viewControlAspect.Show.Add(showRequestEntity);

                showViewRequest.Root = request.Root;
                showViewRequest.View = request.Radius;
                showViewRequest.Size = request.Size;
                showViewRequest.Destination = request.Destination;
            }
        }
    }
}