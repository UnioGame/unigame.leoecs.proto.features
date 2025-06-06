namespace UniGame.Ecs.Proto.Gameplay.Damage.Systems
{
    using System;
    using Aspects;
    using Characteristics.Shield.Aspects;
    using Components.Request;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

    /// <summary>
    /// reduce damage by shield value
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CheckDamageShieldSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private DamageAspect _damageAspect;
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

                if (!_shieldCharacteristicAspect.Shield.Has(destinationEntity)) continue;

                ref var shield = ref _shieldCharacteristicAspect.Shield.Get(destinationEntity);
                var shieldValue = shield.Value;
                var shieldDamage = Mathf.Min(shieldValue, request.Value);

                var shieldRequestEntity = _world.NewEntity();
                ref var shieldRequest = ref _shieldCharacteristicAspect.ChangeShield.Add(shieldRequestEntity);
                shieldRequest.Source = request.Source;
                shieldRequest.Destination = request.Destination;
                shieldRequest.Value = -shieldDamage;

                request.Value -= shieldDamage;
            }
        }
    }
}