namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Components;
    using Data;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class CreateEffectGlobalRootsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private EffectGlobalAspect _effectGlobalAspect;
        private EffectsRootData _configuration;

        private ProtoIt _filter = It
            .Chain<EffectGlobalRootMarkerComponent>()
            .End();

        public void Run()
        {
            if (!_filter.IsEmptySlow()) return;

            var globalRoot = _world.NewEntity();
            ref var globalRootMarker = ref _effectGlobalAspect.Global.Add(globalRoot);
            ref var transforms = ref _effectGlobalAspect.Transforms.Add(globalRoot);
            ref var configuration = ref _effectGlobalAspect.Configuration.Add(globalRoot);

            transforms.Value = new Transform[_configuration.roots.Length];
            configuration.Value = _configuration;
        }
    }
}