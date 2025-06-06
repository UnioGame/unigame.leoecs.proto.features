namespace UniGame.Ecs.Proto.Characteristics.Shield.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

    /// <summary>
    /// System for processing shield requests in a game.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessShieldSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private ShieldCharacteristicAspect _aspect;

        private ProtoIt _filter = It
            .Chain<ChangeShieldRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _aspect.ChangeShield.Get(entity);
                
                if (!request.Destination.Unpack(_world, out var destinationEntity)
                    || !_aspect.Shield.Has(destinationEntity)) continue;

                ref var shield = ref _aspect.Shield.Get(destinationEntity);
                shield.Value += request.Value;

                if (shield.Value <= 0.0f || Mathf.Approximately(shield.Value, 0.0f))
                    _aspect.Shield.Del(destinationEntity);
            }
        }
    }
}