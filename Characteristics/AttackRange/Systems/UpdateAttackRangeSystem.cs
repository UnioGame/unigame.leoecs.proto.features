namespace UniGame.Ecs.Proto.Characteristics.CriticalChance.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.Ecs.Proto.Characteristics.Base.Components;

    /// <summary>
    /// Характеристика дальности атаки изменилась: переносит новое значение в AttackRangeComponent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class UpdateAttackRangeChangedSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AttackRangeAspect _aspect;
        
        private ProtoIt _filter = It
            .Chain<CharacteristicChangedComponent<AttackRangeComponent>>()
            .Inc<CharacteristicComponent<AttackRangeComponent>>()
            .Inc<AttackRangeComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var characteristicComponent = ref _aspect.Characteristic.Get(entity);
                ref var valueComponent = ref _aspect.AttackRange.Get(entity);
                valueComponent.Value = characteristicComponent.Value;
            }
        }
    }
}