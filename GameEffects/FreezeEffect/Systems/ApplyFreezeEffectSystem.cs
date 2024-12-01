namespace UniGame.Ecs.Proto.GameEffects.FreezeEffect.Systems
{
    using System;
    using Aspects;
    using Components.Requests;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// Add a freeze effect to the target
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ApplyFreezeEffectSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private FreezeEffectAspect _freezeEffectAspect;

        private ProtoIt _filter = It
            .Chain<ApplyFreezeTargetEffectRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var applyFreezeEffectRequest = ref _freezeEffectAspect.ApplyFreezeTarget.Get(entity);
                if (!applyFreezeEffectRequest.Destination.Unpack(_world, out var target))
                    continue;
                //TODO: duplicate add component
                ref var freezeTargetComponent = ref _freezeEffectAspect.FreezeTargetEffect.GetOrAddComponent(target);
                freezeTargetComponent.DumpTime = applyFreezeEffectRequest.DumpTime;
                freezeTargetComponent.Source = applyFreezeEffectRequest.Source;
            }
        }
    }
}