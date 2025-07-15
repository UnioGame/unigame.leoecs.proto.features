namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using UniGame.Proto.Ownership;
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
    public sealed class ProcessEffectViewOwnerSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        
        private ProtoIt _filter = It
            .Chain<EffectViewComponent>()
            .Inc<OwnerDestroyedEvent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _effectAspect.DestroyView.TryAdd(entity);
            }
        }
    }
}