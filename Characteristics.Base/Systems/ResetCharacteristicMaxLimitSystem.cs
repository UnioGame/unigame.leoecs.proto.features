namespace UniGame.Ecs.Proto.Characteristics.Base.Systems
{
    using System;
    using Aspects;
    using Components;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// reset max value of characteristic
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ResetCharacteristicMaxLimitSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private ProtoIt _filter = It.Chain<ResetCharacteristicMaxLimitSelfRequest>()
            .Inc<CharacteristicDefaultValueComponent>()
            .Inc<MaxValueComponent>()
            .End();
        
        private CharacteristicsAspect _characteristicsAspect;
        private ModificationsAspect _modificationsAspect;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var maxComponent = ref _characteristicsAspect.MaxValue.Get(entity);
                ref var defaultComponent = ref _characteristicsAspect.DefaultValue.Get(entity);

                maxComponent.Value = defaultComponent.MaxValue;

                _characteristicsAspect.Recalculate.GetOrAddComponent(entity);
            }
        }
    }
}