namespace UniGame.Ecs.Proto.Ability.SubFeatures.Visuals.Systems
{
    using System;
    using Ability.Aspects;
    using Aspects;
    using Common.Components;
    using Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using LeoEcs.Bootstrap.Runtime.Abstract;
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
    public class DelayVisualsSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;

        private AbilityVisualsAspects _abilityVisualsAspects;
        private TimerAspect _timerAspect;
        private OwnershipAspect _ownershipAspect;
        private AbilityAspect _abilityAspect;
        
        private ProtoIt _abilityVisualsFilter = It
            .Chain<AbilityVisualsComponent>()
            .Inc<AbilityStartUsingSelfEvent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var abilityEntity in _abilityVisualsFilter)
            {
                if (!_ownershipAspect.OwnerLink.Has(abilityEntity))
                {
                    continue;
                }

                ref var abilityOwnerLink = ref _ownershipAspect.OwnerLink.Get(abilityEntity);
                if (!abilityOwnerLink.Value.Unpack(_world, out var abilityOwnerEntity))
                {
                    continue;
                }
                
                ref var abilityVisualsComponent = ref _abilityVisualsAspects.AbilityVisuals.Get(abilityEntity);
                
                var delayedVisualsSpawnEntity = _world.NewEntity();
                ref var delayedVisualsSpawnComponent = ref _abilityVisualsAspects.DelayedSpawn.Add(delayedVisualsSpawnEntity);
                delayedVisualsSpawnComponent.assetIdentification = abilityVisualsComponent.assetIdentification;
                delayedVisualsSpawnComponent.spawnPosition = abilityVisualsComponent.spawnPosition;
                delayedVisualsSpawnComponent.boneBound = abilityVisualsComponent.boneBound;
                
                ProtoEntity targetEntity = default;
                if (abilityVisualsComponent.targetSource)
                {
                    targetEntity = abilityOwnerEntity;
                }
                else
                {
                    ref var targetComponent = ref _abilityAspect.Targets.Get(abilityOwnerEntity);
                    if (targetComponent.Count > 0 &&
                        targetComponent.Entities[0].Unpack(_world, out var unpackedTargetEntity))
                    {
                        targetEntity = unpackedTargetEntity;
                    }
                }
                
                delayedVisualsSpawnComponent.target = targetEntity.PackEntity(_world);
                
                ref var cooldownComponent = ref _timerAspect.Cooldown.Add(delayedVisualsSpawnEntity);
                cooldownComponent.Value = abilityVisualsComponent.spawnDelay;
                _timerAspect.Restart.Add(delayedVisualsSpawnEntity);
                
                _ownershipAspect.AddChild(abilityEntity, delayedVisualsSpawnEntity);
            }
        }
    }
}