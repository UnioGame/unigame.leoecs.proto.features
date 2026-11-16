namespace UniGame.Ecs.Proto.Gameplay.Damage.Systems
{
    using System;
    using Aspects;
    using Characteristics.Block.Aspects;
    using Components.Events;
    using Components.Request;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Бросает шанс блока: заблокировал — урон отменяется и кидается событие BlockedDamageEvent.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CheckDamageBlockSystem : IProtoRunSystem
    {
        private readonly int _minBlock;
        private readonly int _maxBlock;

        private ProtoWorld _world;
        private DamageAspect _damageAspect;
        private BlockAspect _blockAspect;

        private ProtoIt _filter = It
            .Chain<ApplyDamageRequest>()
            .End();


        public CheckDamageBlockSystem(int minBlock = 0, int maxBlock = 100)
        {
            _minBlock = minBlock;
            _maxBlock = maxBlock;
        }

        public void Run()
        {
            foreach (var requestEntity in _filter)
            {
                ref var request = ref _damageAspect.ApplyDamage.Get(requestEntity);
                if (!request.Destination.Unpack(_world, out var destinationEntity))
                    continue;

                if (!request.Source.Unpack(_world, out var sourceEntity))
                    continue;

                var effectorAlive = request.Effector.Unpack(_world, out var effectorEntity);
                var isBlockableEffector = effectorAlive && !_damageAspect.BlockableDamage.Has(effectorEntity);

                if (!_blockAspect.Block.Has(destinationEntity))
                    continue;

                if (!_damageAspect.BlockableDamage.Has(sourceEntity) &&
                    !_damageAspect.BlockableDamage.Has(requestEntity) &&
                    !isBlockableEffector)
                    continue;

                ref var blockComponent = ref _blockAspect.Block.Get(destinationEntity);
                var blockChance = blockComponent.Value;
                var chance = Random.Range(_minBlock, _maxBlock);
                var isBlocked = chance < blockChance;

                if (!isBlocked) continue;

                var eventEntity = _world.NewEntity();
                ref var missedEvent = ref _world.AddComponent<BlockedDamageEvent>(eventEntity);
                missedEvent.Source = request.Source;
                missedEvent.Destination = request.Destination;

                _damageAspect.ApplyDamage.Del(requestEntity);
            }
        }
    }
}