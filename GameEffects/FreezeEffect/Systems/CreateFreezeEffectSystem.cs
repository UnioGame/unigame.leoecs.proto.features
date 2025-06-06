namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Systems
{
    using System;
    using Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Create a request to use the freeze effect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateFreezeEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezeEffectAspect _freezeEffectAspect;
        private EffectAspect _effectAspect;

        private ProtoIt _filter = It
            .Chain<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .Inc<FreezeEffectComponent>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var effectComponent = ref _effectAspect.Effect.Get(entity);
                ref var effectDurationComponent = ref _effectAspect.Duration.Get(entity);
                ref var freezeRequest = ref _freezeEffectAspect.ApplyFreezeTarget.Add(_world.NewEntity());
                
                freezeRequest.Source = effectComponent.Source;
                freezeRequest.Destination = effectComponent.Destination;
                freezeRequest.DumpTime = effectDurationComponent.CreatingTime + effectDurationComponent.Duration;
            }
        }
    }
}