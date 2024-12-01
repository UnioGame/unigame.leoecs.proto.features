namespace UniGame.Ecs.Proto.Ability
{
    using System;
    using AbilityUtilityView.Aspects;
    using AbilityUtilityView.Components;
    using AbilityUtilityView.Radius.AggressiveRadius.Components;
    using Game.Ecs.Core.Death.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAggressiveAbilityRadiusWhenDeadSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityUtilityViewAspect _abilityUtilityViewAspect;
        
        private ProtoIt _filter = It
            .Chain<DestroyComponent>()
            .Inc<VisibleUtilityViewComponent>()
            .Inc<AggressiveRadiusViewStateComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var state = ref _abilityUtilityViewAspect.AggressiveRadiusViewStat.Get(entity);
                foreach (var packedEntity in state.Entities)
                {
                    var hideRequestEntity = _world.NewEntity();
                    ref var hideRequest = ref _abilityUtilityViewAspect.HideRadius.Add(hideRequestEntity);

                    hideRequest.Source = entity.PackEntity(_world);
                    hideRequest.Destination = packedEntity;
                }
            }
        }
    }
}