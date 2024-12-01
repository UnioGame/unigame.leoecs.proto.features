namespace UniGame.Ecs.Proto.Ability.AbilityUtilityView.Radius.Systems
{
    using System;
    using Aspects;
    using Common.Components;
    using Component;
    using LeoEcs.Bootstrap.Runtime.Attributes;
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
    public sealed class ProcessNotInHandAbilityRadiusSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        
        private ProtoItExc _filter = It
            .Chain<RadiusViewStateComponent>()
            .Inc<AbilityIdComponent>()
            .Exc<AbilityInHandComponent>()
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