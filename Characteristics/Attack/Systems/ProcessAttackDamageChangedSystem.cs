namespace UniGame.Ecs.Proto.Characteristics.AttackDamage.Systems
{
    using System;
    using Aspects;
    using Base.Components;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Характеристика урона атаки изменилась: переносит новые значения в AttackDamageComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAttackDamageChangedSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AttackDamageAspect _attackDamageAspect;

        private ProtoIt _filter = It
            .Chain<CharacteristicChangedComponent<AttackDamageComponent>>()
            .Inc<CharacteristicComponent<AttackDamageComponent>>()
            .Inc<AttackDamageComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _attackDamageAspect.Value.Get(entity);
                ref var attackDamageComponent = ref _attackDamageAspect.Characteristic.Get(entity);
                attackDamageComponent.Value = characteristicComponent.Value;
                attackDamageComponent.MaxValue = characteristicComponent.MaxValue;
                attackDamageComponent.MinValue = characteristicComponent.MinValue;
                attackDamageComponent.BaseValue = characteristicComponent.BaseValue;
            }
        }
    }
}