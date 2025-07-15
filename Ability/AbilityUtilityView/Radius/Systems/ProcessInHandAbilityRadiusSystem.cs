namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Ability.Aspects;
    using Aspects;
    using Characteristics.Radius;
    using Characteristics.Radius.Component;
    using Common.Components;
    using Component;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessInHandAbilityRadiusSystem : IProtoRunSystem
    {
        private const float TimeToShow = 0.5f;
        private ProtoWorld _world;
        private AbilityAspect _abilityAspect;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        private OwnershipAspect _ownershipAspect;
        private RadiusCharacteristicAspect _radiusCharacteristicAspect;

        private ProtoIt _filter = It
            .Chain<AbilityInHandComponent>()
            .Inc<AbilityActiveTimeComponent>()
            .Inc<RadiusComponent>()
            .Inc<OwnerLinkComponent>()
            .Inc<RadiusViewComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(entity);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                    continue;

                if (!_abilityUtilityViewAspect.VisibleUtilityView.Has(ownerEntity))
                    continue;

                var packedEntity = _world.PackEntity(entity);
                ref var activeTime = ref _abilityAspect.AbilityActiveTimeComponent.Get(entity);

                if (activeTime.Time < TimeToShow && !Mathf.Approximately(activeTime.Time, TimeToShow))
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _abilityUtilityViewAspect.HideRadius.Add(hideRequestEntity);

                    hideRequest.Source = packedEntity;
                    hideRequest.Destination = ownerLinkComponent.Value;

                    continue;
                }

                ref var radius = ref _radiusCharacteristicAspect.Radius.Get(entity);

                var showRequestEntity = _world.NewEntity();
                ref var showRequest = ref _abilityUtilityViewAspect.ShowRadius.Add(showRequestEntity);

                showRequest.Source = packedEntity;
                showRequest.Destination = ownerLinkComponent.Value;

                ref var radiusView = ref _abilityUtilityViewAspect.RadiusView.Get(entity);

                showRequest.Radius = radiusView.RadiusView;
                showRequest.Root = radiusView.Root;

                var size = radius.Value * 2.0f;
                showRequest.Size = new Vector3(size, size, size);
            }
        }
    }
}