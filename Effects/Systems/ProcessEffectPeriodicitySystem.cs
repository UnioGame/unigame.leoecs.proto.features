namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Game.Ecs.Time.Service;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    /// <summary>
    /// System for processing periodicity of effects
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessEffectPeriodicitySystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;

        private ProtoItExc _filter = It
            .Chain<EffectComponent>()
            .Inc<EffectPeriodicityComponent>()
            .Exc<DestroyEffectSelfRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var periodicity = ref _effectAspect.Periodicity.Get(entity);
                if (periodicity.Periodicity < 0.0f)
                {
                    if (periodicity.LastApplyingTime < Time.time)
                    {
                        periodicity.LastApplyingTime = float.MaxValue;
                        _effectAspect.Apply.TryAdd(entity);
                    }

                    continue;
                }

                var nextApplyingTime = periodicity.LastApplyingTime + periodicity.Periodicity;
                if (GameTime.Time < nextApplyingTime && !Mathf.Approximately(nextApplyingTime, GameTime.Time))
                    continue;

                periodicity.LastApplyingTime = GameTime.Time;

                _effectAspect.Apply.TryAdd(entity);
            }
        }
    }
}