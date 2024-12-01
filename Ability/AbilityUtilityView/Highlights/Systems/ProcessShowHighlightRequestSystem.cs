namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Highlights.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Core.Aspects;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using ViewControl.Aspects;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessShowHighlightRequestSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        private FeaturesAspect _featuresAspect;
        private ViewControlAspect _viewControlAspect;

        private ProtoIt _filter = It
            .Chain<ShowHighlightRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _abilityUtilityViewAspect.ShowHighlight.Get(entity);
                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                ref var state = ref _abilityUtilityViewAspect.HighlightState.GetOrAddComponent(sourceEntity);
                if (state.Highlights.ContainsKey(request.Destination))
                    continue;

                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!_abilityUtilityViewAspect.Highlight.Has(destinationEntity) ||
                    !_featuresAspect.EntityAvatar.Has(destinationEntity))
                    continue;

                ref var highlight = ref _abilityUtilityViewAspect.Highlight.Get(destinationEntity);
                ref var avatar = ref _featuresAspect.EntityAvatar.Get(destinationEntity);

                var showRequestEntity = _world.NewEntity();
                ref var showViewRequest = ref _viewControlAspect.Show.Add(showRequestEntity);

                showViewRequest.Root = avatar.Feet;
                showViewRequest.View = highlight.Highlight;
                var size = avatar.Bounds.Radius * 2.0f;
                showViewRequest.Size = new Vector3(size, size, size);

                showViewRequest.Destination = request.Destination;

                state.Highlights.Add(request.Destination, highlight.Highlight);
            }
        }
    }
}