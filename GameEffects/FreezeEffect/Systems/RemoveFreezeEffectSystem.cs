namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

    /// <summary>
    /// Remove freeze effect
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RemoveFreezeEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezeEffectAspect _freezeEffectAspect;
        
        private ProtoIt _filter = It
            .Chain<RemoveFreezeTargetEffectRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var removeFreezeTargetEffectRequest = ref _freezeEffectAspect.RemoveFreezeTarget.Get(entity);
                if (!removeFreezeTargetEffectRequest.Target.Unpack(_world, out var target))
                    continue;
                _freezeEffectAspect.FreezeTargetEffect.Del(target);
            }
        }
    }
}