namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Aspects;
    using Component;
    using UniGame.Proto.Ownership;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAbilityRadiusWhenOwnerDeadSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        
        private ProtoIt _filter= It
            .Chain<OwnerDestroyedEvent>()
            .Inc<RadiusViewStateComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var radiusState = ref _abilityUtilityViewAspect.RadiusViewState.Get(entity);
                foreach (var packedEntity in radiusState.RadiusViews)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _abilityUtilityViewAspect.HideRadius.Add(hideRequestEntity);
                
                    hideRequest.Source = _world.PackEntity(entity);
                    hideRequest.Destination = packedEntity.Key;
                }
            }
        }
    }
}