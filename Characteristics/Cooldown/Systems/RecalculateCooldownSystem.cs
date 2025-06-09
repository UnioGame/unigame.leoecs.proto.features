namespace UniGame.Ecs.Proto.Characteristics.Cooldown.Systems
{
    using System;
    using Aspects;
    using Characteristics;
    using Components;
    using LeoEcs.Bootstrap;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Timer.Components;

    /// <summary>
    /// System that recalculates the cooldown value of entities.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class RecalculateCooldownSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private TimerAspect _timerAspect;
        private CooldownAspect _cooldownAspect;
        
        private ProtoIt _filter = It
            .Chain<RecalculateCooldownSelfRequest>()
            .Inc<BaseCooldownComponent>()
            .Inc<CooldownComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var baseCooldown = ref _cooldownAspect.BaseCooldown.Get(entity);
                ref var cooldown = ref _timerAspect.Cooldown.Get(entity);

                cooldown.Value = baseCooldown.Modifications.Apply(baseCooldown.Value);
            }
        }
    }
}