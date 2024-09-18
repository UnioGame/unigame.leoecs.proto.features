namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Systems
{
    using System;
    using System.Runtime.CompilerServices;
    using Ability.Aspects;
    using Aspects;
    using Components;
    using Effects;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using GameResources.Systems;
    using LeoEcs.Bootstrap.Runtime.Abstract;
    using LeoEcs.Timer.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateAbilityVisualsSystem : IProtoInitSystem, IProtoRunSystem
    {
        private GameSpawnTools _gameSpawnTools;
        
        private ProtoWorld _world;
        
        private AbilityVisualsAspects _abilityVisualsAspects;
        private OwnershipAspect _ownershipAspect;
        private AbilityAspect _abilityAspect;
        private UnityAspect _unityAspect;
        
        private ProtoItExc _delayedSpawnFilter = It
            .Chain<DelayedVisualsSpawnComponent>()
            .Inc<CooldownCompleteComponent>()
            .Exc<PrepareToDeathComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _gameSpawnTools = _world.GetGlobal<GameSpawnTools>();
        }

        public void Run()
        {
            foreach (var delayedSpawnEntity in _delayedSpawnFilter)
            {
                ref var delayedVisualsSpawnComponent = ref _abilityVisualsAspects.DelayedSpawn.Get(delayedSpawnEntity);
                var resourceId = delayedVisualsSpawnComponent.assetIdentification;
                var spawnPositionType = delayedVisualsSpawnComponent.spawnPosition;
                var isBoneBound = delayedVisualsSpawnComponent.boneBound;
                var targetEntity = delayedVisualsSpawnComponent.target;

                if (!targetEntity.Unpack(_world, out var unpackedTargetEntity))
                {
                    delayedSpawnEntity.Kill(_world);
                    continue;
                }
                
                var spawnPositionTransform = unpackedTargetEntity.GetViewInstance(_world, spawnPositionType);
                var targetTransform = isBoneBound ? spawnPositionTransform : null;
                _gameSpawnTools.Spawn(resourceId, spawnPositionTransform.position/*, targetTransform*/);
                
                delayedSpawnEntity.Kill(_world);
            }
        }
    }
}