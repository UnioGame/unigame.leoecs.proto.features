namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using Runtime.ObjectPool.Extensions;
    
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class DestroyEffectViewSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        
        private ProtoIt _filter = It
            .Chain<EffectViewComponent>()
            .Inc<DestroyEffectViewSelfRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var view = ref _effectAspect.View.Get(entity);
                if (view.ViewInstance != null)
                {
                    view.ViewInstance.Despawn();
                }
                
                _world.DelEntity(entity);
            }
        }
    }
}