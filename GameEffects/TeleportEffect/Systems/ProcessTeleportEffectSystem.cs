namespace UniGame.Ecs.Proto.GameEffects.TeleportEffect.Systems
{
    using System;
    using Components;
    using Effects.Components;
    using Game.Ecs.Core;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.Mathematics;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessTeleportEffectSystem : IProtoRunSystem,IProtoInitSystem
    {
        private ProtoIt _filter;
        private ProtoWorld _world;
        
        private ProtoPool<EffectComponent> _effectPool;
        private ProtoPool<TransformComponent> _transformPool;
        private ProtoPool<EntityAvatarComponent> _avatarPool;

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world
                .Filter<TeleportEffectComponent>()
                .Inc<EffectComponent>()
                .Inc<ApplyEffectSelfRequest>()
                .End();
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effect = ref _effectPool.Get(entity);
                if (!effect.Source.Unpack(_world, out var sourceEntity)
                    || !_transformPool.Has(sourceEntity) || !_avatarPool.Has(sourceEntity))
                    continue;

                if (!effect.Destination.Unpack(_world, out var destinationEntity)
                    || !_transformPool.Has(destinationEntity) || !_avatarPool.Has(destinationEntity))
                    continue;

                ref var sourceTransform = ref _transformPool.Get(sourceEntity);
                float3 sourcePosition = sourceTransform.Value.position;

                ref var sourceAvatar = ref _avatarPool.Get(sourceEntity);

                ref var destinationTransform = ref _transformPool.Get(destinationEntity);
                var destinationPosition = destinationTransform.Value.position;

                ref var destinationAvatar = ref _avatarPool.Get(destinationEntity);

                var teleportPosition = EntityHelper
                    .GetPoint(sourcePosition,  destinationPosition, ref destinationAvatar.Bounds);
                
                var point = teleportPosition - sourcePosition;
                var direction = math.normalize(point);
                sourceTransform.Value.position = teleportPosition - direction * sourceAvatar.Bounds.Radius * 4.0f;
            }
        }
    }
}