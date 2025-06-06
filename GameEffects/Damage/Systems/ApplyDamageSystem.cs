namespace UniGame.Ecs.Proto.Gameplay.Damage.Systems
{
    using System;
    using Aspects;
    using Components.Request;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Characteristics.Health.Aspects;
    using Characteristics.Shield.Aspects;
    using LeoEcs.Shared.Extensions;
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
    public sealed class ApplyDamageSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DamageAspect _damageAspect;
        private HealthAspect _healthAspect;
        private ShieldCharacteristicAspect _shieldCharacteristicAspect;
        
        private ProtoIt _filter = It
            .Chain<ApplyDamageRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _damageAspect.ApplyDamage.Get(entity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                var healthDamage = request.Value;
                
                var healthRequestEntity = _world.NewEntity();
                ref var healthRequest = ref _healthAspect.ChangeBase.Add(healthRequestEntity);
                healthRequest.Source = request.Source;
                healthRequest.Target = request.Destination;
                healthRequest.Value = -healthDamage;

                request.Source.Unpack(_world, out var sourceEntity);
                
                var eventEntity = _world.NewEntity();
                ref var madeDamage = ref _damageAspect.MadeDamage.Add(eventEntity);
                madeDamage.Value = request.Value;
                madeDamage.Source = request.Source;
                madeDamage.Destination = request.Destination;
                madeDamage.IsCritical = request.IsCritical;

                if (!request.IsCritical) continue;
                
                ref var criticalEventComponent = ref _damageAspect.CriticalDamage.Add(eventEntity);
                criticalEventComponent.Value = request.Value;
                criticalEventComponent.Source = request.Source;
                criticalEventComponent.Destination = request.Destination;
            }
        }
    }
}