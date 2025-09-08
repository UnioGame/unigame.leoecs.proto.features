namespace UniGame.Ecs.Proto.GameEffects.StunEffect.Systems
{
    using System;
    using Characteristics.Stun.Aspects;
    using Components;
    using Effects.Aspects;
    using Effects.Components;
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
    public class ProcessStunEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;

        private StunAspect _stunAspect;
        private EffectAspect _effectAspect;
        
        private ProtoIt _effectFilter = It
            .Chain<StunEffectComponent>()
            .Inc<EffectComponent>()
            .Inc<ApplyEffectSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var effectEntity in _effectFilter)
            {
                ref var effectComponent = ref _effectAspect.Effect.Get(effectEntity);
                if (!effectComponent.Destination.Unpack(_world, out var destinationEntity))
                {
                    continue;
                }

                _stunAspect.Stun.GetOrAddComponent(destinationEntity);
            }
        }
    }
}