namespace UniGame.Ecs.Proto.Effects.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    
#if ENABLE_IL2CP
	using Unity.IL2CPP.CompilerServices;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ValidateEffectsListSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private List<ProtoPackedEntity> _cacheList = new();

        private EffectAspect _effectAspect;

        private ProtoIt _filter = It
            .Chain<EffectsListComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var list = ref _effectAspect.List.Get(entity);
                _cacheList.Clear();
                _cacheList.AddRange(list.Effects);
                
                list.Effects.Clear();
                
                foreach (var effectPackedEntity in _cacheList)
                {
                    if(!effectPackedEntity.Unpack(_world, out _))
                        continue;
                    
                    list.Effects.Add(effectPackedEntity);
                }
            }
        }
    }
}