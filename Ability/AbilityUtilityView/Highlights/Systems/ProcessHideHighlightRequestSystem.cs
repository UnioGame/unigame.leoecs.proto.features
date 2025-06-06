namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using ViewControl.Aspects;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessHideHighlightRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        private ViewControlAspect _viewControlAspect;

        private ProtoIt _filter = It
            .Chain<HideHighlightRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _abilityUtilityViewAspect.HideHighlight.Get(entity);
                if (!request.Source.Unpack(_world, out var sourceEntity) ||
                    !_abilityUtilityViewAspect.HighlightState.Has(sourceEntity))
                    continue;

                ref var state = ref _abilityUtilityViewAspect.HighlightState.Get(sourceEntity);
                if (!state.Highlights.Remove(request.Destination, out var view))
                    continue;

                if (state.Highlights.Count == 0)
                    _abilityUtilityViewAspect.HighlightState.Del(sourceEntity);

                var hideRequestEntity = _world.NewEntity();
                ref var hideViewRequest = ref _viewControlAspect.Hide.Add(hideRequestEntity);

                hideViewRequest.View = view;
                hideViewRequest.Destination = request.Destination;
            }
        }
    }
}