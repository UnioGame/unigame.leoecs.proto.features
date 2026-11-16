namespace UniGame.Ecs.Proto.Characteristics.AttackSpeed.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Characteristics.Base.Components;

    /// <summary>
    /// Характеристика скорости атаки изменилась: переносит новое значение в AttackSpeedComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class UpdateAttackSpeedChangedSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AttackSpeedCharacteristicAspect _aspect;
        
        private ProtoIt _filter = It
            .Chain<CharacteristicChangedComponent<AttackSpeedComponent>>()
            .Inc<CharacteristicComponent<AttackSpeedComponent>>()
            .Inc<AttackSpeedComponent>()
            .Inc<AttackSpeedCooldownTypeComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _aspect.AttackSpeedCharacteristic.Get(entity);
                ref var attackSpeedComponent = ref _aspect.AttackSpeed.Get(entity);
                attackSpeedComponent.Value = characteristicComponent.Value;
            }
        }
    }
}