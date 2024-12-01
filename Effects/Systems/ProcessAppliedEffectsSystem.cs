namespace UniGame.Ecs.Proto.Effects.Systems
{
    using Aspects;
    using Components;
     
    using System;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ProcessAppliedEffectsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectAspect _effectAspect;
        
        private ProtoItExc _filter = It
            .Chain<ApplyEffectSelfRequest>()
            .Exc<DestroyEffectSelfRequest>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                _effectAspect.EffectAppliedSelfEvent.Add(entity);
            }
        }
    }
}