namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Systems
{
    using System;
    using Ability.Aspects;
    using Ability.Common.Components;
    using Aspects;
    using Components;
    using Game.Code.Timeline;
    using Game.Ecs.Core.Aspects;
    using Game.Ecs.Core.Components;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UnityEngine;

    /// <summary>
    /// Stops animation and ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ProcessFreezeEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezeEffectAspect _freezeEffectAspect;
        private FeaturesAspect _featuresAspect;
        private AbilityAspect _abilityAspect;
        private OwnershipAspect _ownershipAspect;
        
        private ProtoIt _filter = It
            .Chain<FreezeTargetEffectComponent>()
            .Inc<PlayableDirectorComponent>()
            .Inc<AbilityMapComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var freezeTargetEffectComponent = ref _freezeEffectAspect.FreezeTargetEffect.Get(entity);
                ref var abilityComponent = ref _abilityAspect.AbilityMapComponent.Get(entity);
                ref var playableDirectorComponent = ref _featuresAspect.PlayableDirector.Get(entity);
                var playableDirector = playableDirectorComponent.Value;
                if (!playableDirector)
                    continue;
                var dumpTime = freezeTargetEffectComponent.DumpTime;
                if (Time.time >= dumpTime || Mathf.Approximately(dumpTime, Time.time) || _ownershipAspect.PrepareToDeath.Has(entity))
                {
                    var requestEntity = _world.NewEntity();
                    ref var requestComponent = ref _freezeEffectAspect.RemoveFreezeTarget.Add(requestEntity);
                    requestComponent.Target = _world.PackEntity(entity);

                    playableDirector.SetRootSpeed(1);
                    
                    foreach (var ability in abilityComponent.Abilities)
                    {
                        ref var request = ref _abilityAspect.RemovePauseAbilityRequest.Add(_world.NewEntity());
                        request.AbilityEntity = ability;
                    }
                    continue;
                }
                
                foreach (var ability in abilityComponent.Abilities)
                {
                    ref var request = ref _abilityAspect.PauseAbilityRequest.Add(_world.NewEntity());
                    request.AbilityEntity = ability;
                }

                playableDirector.SetRootSpeed(0);
            }
        }
    }
}