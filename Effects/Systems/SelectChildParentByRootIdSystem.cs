namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Data;
    using Game.Modules.UnioModules.UniGame.CoreModules.UniGame.Core.Runtime.Extension;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// select parent by effect root id
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;
 
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class SelectChildParentByRootIdSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private EffectAspect _effectAspect;
        private EffectsRootData _effectsRootData;
        private EffectRootKey[] _roots;

        private ProtoItExc _filter = It
            .Chain<EffectAppliedSelfEvent>()
            .Inc<EffectRootIdComponent>()
            .Exc<EffectParentComponent>()
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
                ref var effect = ref _effectAspect.Effect.Get(entity);
                if(!effect.Destination.Unpack(_world, out var destinationEntity))
                    continue;
                
                if(!_effectAspect.Position.Has(destinationEntity))
                    continue;
                
                ref var idComponent = ref _effectAspect.EffectRootId.Get(entity);
                var key = _roots[idComponent.Value];
                if(!key.isChild) continue;
                
                ref var targetTransformComponent = ref _effectAspect.Transform.Get(destinationEntity);
                var transform = targetTransformComponent.Value;
                var gameObject = transform.gameObject;
                
                ref var parentComponent = ref _effectAspect.Parent.Add(entity);
                var parentValue = gameObject.FindChildGameObject(key.objectName);         
                parentComponent.Value = parentValue == null ? null : parentValue.transform;
            }
        }
    }
}