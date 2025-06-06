namespace UniGame.Ecs.Proto.Gameplay.Dodge.Systems
{
    using System;
    using Aspects;
    using Characteristics.Dodge.Aspects;
    using Damage.Aspects;
    using Damage.Components.Request;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CheckDamageDodgeSystem : IProtoRunSystem
    {
        private readonly int _minDodge;
        private readonly int _maxDodge;

        private ProtoWorld _world;
        private DamageAspect _damageAspect;
        private DodgeAspect _dodgeAspect;
        private DodgeCharacteristicAspect _dodgeCharacteristicAspect;

        private ProtoIt _filter = It
            .Chain<ApplyDamageRequest>()
            .End();

        public CheckDamageDodgeSystem(int minDodge = 0, int maxDodge = 100)
        {
            _minDodge = minDodge;
            _maxDodge = maxDodge;
        }

        public void Run()
        {
            foreach (var requestEntity in _filter)
            {
                ref var request = ref _damageAspect.ApplyDamage.Get(requestEntity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!_dodgeCharacteristicAspect.Dodge.Has(destinationEntity))
                    continue;

                ref var dodgeComponent = ref _dodgeCharacteristicAspect.Dodge.Get(destinationEntity);
                var dodgeChance = dodgeComponent.Value;
                var chance = Random.Range(_minDodge, _maxDodge);
                var isDodge = chance < dodgeChance;

                if (!isDodge) continue;

                var eventEntity = _world.NewEntity();
                ref var missedEvent = ref _dodgeAspect.Missed.Add(eventEntity);
                missedEvent.Source = request.Source;
                missedEvent.Destination = request.Destination;

                _damageAspect.ApplyDamage.Del(requestEntity);
            }
        }
    }
}