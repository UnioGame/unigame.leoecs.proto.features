namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using Aspects;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Modules.leoecs.proto.tools.Ownership.Aspects;
    using Game.Modules.leoecs.proto.tools.Ownership.Extensions;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    /// <summary>
    /// Show view effect by effect data
    /// </summary>
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;
 
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ShowEffectViewSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoWorld _world;
        
        private EffectAspect _effectAspect;
        private EffectViewAspect _effectViewAspect;
        private OwnershipAspect _ownershipAspect;
        
        private EffectViewResult[] _effectViewResults = new EffectViewResult[EcsEffectsConfiguration.MAX_EFFECTS_COUNT];
        private int _counter = 0;
        private ILifeTime _lifeTime;
        
        private ProtoItExc _filter = It
            .Chain<EffectAppliedSelfEvent>()
            .Inc<EffectComponent>()
            .Inc<EffectViewDataComponent>()
            .Inc<EffectDurationComponent>()
            .Exc<EffectShowCompleteComponent>()
            .End();
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.GetWorld();
            _lifeTime = _world.GetWorldLifeTime();
        }
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                if(_counter >= EcsEffectsConfiguration.MAX_EFFECTS_COUNT)
                    break;
                
                ref var effect = ref _effectAspect.Effect.Get(entity);
                
                if(!effect.Destination.Unpack(_world, out var destinationEntity) || 
                   !_effectAspect.Avatar.Has(destinationEntity))
                    continue;

                ref var avatarComponent = ref _effectAspect.Avatar.Get(destinationEntity);
                ref var viewDataComponent = ref _effectAspect.ViewData.Get(entity);
                ref var durationComponent = ref _effectAspect.Duration.Get(entity);
                
                var effectViewDuration = viewDataComponent.LifeTime < 0.0f
                    ? durationComponent.Duration
                    : viewDataComponent.LifeTime;
                
                var size = avatarComponent.Bounds.Radius * 2.0f;
                var packedEntity = _world.PackEntity(entity);

                if (viewDataComponent.View != null &&
                    viewDataComponent.View.RuntimeKeyIsValid())
                {
                    CreateEffectView(packedEntity,viewDataComponent.View,
                            effectViewDuration, size)
                        .Forget();
                }
            }
            
            for (var i = 0; i < _counter; i++)
            {
                ref var effectViewResult = ref _effectViewResults[i];

                ref var sourceEntity = ref effectViewResult.Source;
                if(!sourceEntity.Unpack(_world,out var entity)) continue;
                
                _effectAspect.ShowComplete.GetOrAddComponent(entity);
                ref var parentComponent = ref _effectAspect.Parent.Get(entity);
                var parentTransform = parentComponent.Value;
                
                if(effectViewResult.View == null || parentTransform == null) continue;
                
                ref var viewDataComponent = ref _effectAspect.ViewData.Get(entity);
                ref var effect = ref _effectAspect.Effect.Get(entity);
                
                var effectEntity = _world.NewEntity();
                _effectAspect.Parent.Copy(entity,effectEntity);
                var targetPackEntity =  viewDataComponent.AttachToSource 
                    ? effect.Source 
                    : effect.Destination;
                
                targetPackEntity.AddChild(effectEntity, _world);
                
                var size = effectViewResult.Size <= 0 ? 1 : effectViewResult.Size;
                var deathTime = Time.time + effectViewResult.Duration;
                
                var viewInstance = effectViewResult.View.Spawn(parentTransform);
                viewInstance.transform.localScale = new Vector3(size, size, size);
                viewInstance.transform.localPosition = Vector3.zero;
                viewInstance.SetActive(true);
                
                ref var effectView = ref _effectViewAspect.View.Add(effectEntity);
                effectView.ViewInstance = viewInstance;
                effectView.DeadTime = deathTime;
            }

            _counter = 0;
        }

        private async UniTask CreateEffectView(ProtoPackedEntity source,
            AssetReferenceGameObject view,float duration, float size)
        {
            var viewSource = await view.LoadAssetTaskAsync(_lifeTime);
            
            var effectViewResult = new EffectViewResult()
            {
                Source = source,
                Size = size,
                Duration = duration,
                View = viewSource,
            };
                    
            _effectViewResults[_counter] = effectViewResult;
            _counter++;
        }

    }
    
    [Serializable]
    public struct EffectViewResult
    {
        public ProtoPackedEntity Source;
        public GameObject View;
        public float Duration;
        public float Size;
    }
}