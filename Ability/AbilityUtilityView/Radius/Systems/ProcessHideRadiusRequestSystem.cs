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
    public sealed class ProcessHideRadiusRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        private ViewControlAspect _viewControlAspect;

        private ProtoIt _filter = It
            .Chain<HideRadiusRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _abilityUtilityViewAspect.HideRadius.Get(entity);
                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var state = ref _abilityUtilityViewAspect.RadiusViewState.GetOrAddComponent(sourceEntity);
                if (!state.RadiusViews.Remove(request.Destination, out var radiusView))
                    continue;

                if (state.RadiusViews.Count == 0)
                    _abilityUtilityViewAspect.RadiusViewState.Del(sourceEntity);

                var hideRequestEntity = _world.NewEntity();
                ref var hideViewRequest = ref _viewControlAspect.Hide.Add(hideRequestEntity);

                hideViewRequest.View = radiusView;
                hideViewRequest.Destination = request.Destination;
            }
        }
    }
}