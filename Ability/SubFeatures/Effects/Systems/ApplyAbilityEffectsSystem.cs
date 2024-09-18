namespace Game.Modules.leoecs.proto.features.Ability.SubFeatures.Effects.Systems
{
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using tools.Ownership.Aspects;
    using tools.Ownership.Components;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.Ecs.Proto.Ability.Aspects;
    using UniGame.Ecs.Proto.Ability.Common.Components;
    using UniGame.Ecs.Proto.Effects;
    using UniGame.Ecs.Proto.Effects.Aspects;
    using UniGame.Ecs.Proto.Effects.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyAbilityEffectsSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private AbilityAspect _abilityAspect;
        private EffectAspect _effectAspect;
        private OwnershipAspect _ownershipAspect;

        private ProtoIt _applyEffectsFilter = It
            .Chain<AbilityStartUsingSelfEvent>()
            .Inc<EffectsComponent>()
            .Inc<OwnerLinkComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run()
        {
            foreach (var applyEffectsRequest in _applyEffectsFilter)
            {
                ref var ownerLinkComponent = ref _ownershipAspect.OwnerLink.Get(applyEffectsRequest);
                if (!ownerLinkComponent.Value.Unpack(_world, out var ownerEntity))
                {
                    continue;
                }
                
                ref var targetsComponent = ref _abilityAspect.Targets.Get(ownerEntity);
                if (targetsComponent.Count < 1)
                {
                    continue;
                }

                var target = targetsComponent.Entities[0];
                var source = _world.PackEntity(applyEffectsRequest);
                
                ref var effectsComponent = ref _effectAspect.EffectsComponent.Get(applyEffectsRequest);
                GameLog.Log($"Ability {applyEffectsRequest}: effects applied");
                effectsComponent.Effects.CreateRequests(_world, source, target);
            }
        }
    }
}