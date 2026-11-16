namespace UniGame.Ecs.Proto.Characteristics.Health.Systems
{
    using System;
    using Aspects;
    using Base.Components;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Характеристика здоровья изменилась: переносит новые значения в HealthComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessHealthChangedSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private HealthAspect _healthAspect;

        private ProtoIt _filter = It
            .Chain<CharacteristicChangedComponent<HealthComponent>>()
            .Inc<CharacteristicComponent<HealthComponent>>()
            .Inc<HealthComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _healthAspect.Value.Get(entity);
                ref var healthComponent = ref _healthAspect.Characteristic.Get(entity);
                healthComponent.Value = characteristicComponent.Value;
                healthComponent.MaxValue = characteristicComponent.MaxValue;
                healthComponent.MinValue = characteristicComponent.MinValue;
                healthComponent.BaseValue = characteristicComponent.BaseValue;
            }
        }
    }
}