namespace UniGame.Ecs.Proto.Gameplay.Damage.Systems
{
    using System;
    using Aspects;
    using Characteristics.CriticalMultiplier.Aspects;
    using Components.Request;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// apply damage to target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CalculateCriticalDamageSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DamageAspect _damageAspect;
        private CriticalMultiplierCharacteristicAspect _criticalMultiplierCharacteristicAspect;
        
        private ProtoIt _filter = It
            .Chain<ApplyDamageRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _damageAspect.ApplyDamage.Get(entity);
                
                if (!request.Source.Unpack(_world, out var sourceEntity) ||
                    !request.IsCritical)
                    continue;

                var multiplier = 0f;

                if (_criticalMultiplierCharacteristicAspect.CriticalMultiplier.Has(sourceEntity))
                {
                    ref var criticalMultiplierComponent =
                        ref _criticalMultiplierCharacteristicAspect.CriticalMultiplier.Get(sourceEntity);
                    var characteristicValue = criticalMultiplierComponent.Value;
                    multiplier += characteristicValue;
                }

                multiplier /= 100f;

                var damage = request.Value + request.Value * multiplier;
                request.Value = damage;
            }
        }
    }
}