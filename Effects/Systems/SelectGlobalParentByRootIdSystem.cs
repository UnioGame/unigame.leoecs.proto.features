namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// select parent from global effect targets
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;
 
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelectGlobalParentByRootIdSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private EffectAspect _effectAspect;
        private EffectGlobalAspect _effectGlobalAspect;
        private EffectTargetAspect _targetAspect;
        
        private EffectsRootData _effectsRootData;
        private EffectRootKey[] _roots;
        
        private ProtoItExc _filter = It
            .Chain<EffectAppliedSelfEvent>()
            .Inc<EffectRootIdComponent>()
            .Exc<EffectParentComponent>()
            .End();
        
        private ProtoIt _rootsFilter = It
            .Chain<EffectRootTargetComponent>()
            .Inc<TransformComponent>()
            .Inc<EffectRootIdComponent>()
            .End();
        
        private ProtoIt _globalFilter = It
            .Chain<EffectGlobalRootMarkerComponent>()
            .Inc<EffectRootTransformsComponent>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _effectsRootData = _world.GetGlobal<EffectsRootData>();
            _roots = _effectsRootData.roots;
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var idComponent = ref _effectAspect.EffectRootId.Get(entity);
                ref var parentComponent = ref _effectAspect.Parent.Add(entity);
                
                foreach (var globalEntity in _globalFilter)
                {
                    ref var globalTransformsComponent = ref _effectGlobalAspect.Transforms.Get(globalEntity);
                    var targetParent = globalTransformsComponent.Value[idComponent.Value];
                    parentComponent.Value = targetParent;
                    
                    if(targetParent !=null) break;

                    //rebuild global transforms cache
                    foreach (var rootEntity in _rootsFilter)
                    {
                        ref var rootIdComponent = ref _targetAspect.Id.Get(rootEntity);
                        ref var transformComponent = ref _targetAspect.Transform.Get(rootEntity);
                        var targetTransform = transformComponent.Value;
                        if(targetTransform == null) continue;
                        globalTransformsComponent.Value[rootIdComponent.Value] = transformComponent.Value;
                    }
                    
                    parentComponent.Value =  globalTransformsComponent.Value[idComponent.Value];
                }
            }
        }
    }
}