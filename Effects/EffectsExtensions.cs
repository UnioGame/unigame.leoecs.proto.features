namespace UniGame.Ecs.Proto.Effects
{
    using System.Collections.Generic;
    using Components;
    using Game.Code.Configuration.Runtime.Effects;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using LeoEcs.Shared.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class EffectsExtensions
    {
#if ENABLE_IL2CPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void CreateRequests(this List<IEffectConfiguration> effects, 
            ProtoWorld world, 
            ProtoPackedEntity source,
            ProtoPackedEntity destination)
        {
            if(effects == null) return;
            
            foreach (var effect in effects)
                effect.CreateRequest(world,ref source,ref destination);
        }

#if ENABLE_IL2CPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static ProtoEntity CreateRequest(this IEffectConfiguration effect,
            ProtoWorld world,
            ref ProtoPackedEntity source,
            ref ProtoPackedEntity destination)
        {
            var requestPool = world.GetPool<CreateEffectSelfRequest>();
            var effectsEntity = world.NewEntity();
                
            ref var request = ref requestPool.Add(effectsEntity);
            request.Source = source;
            request.Destination = effect.TargetType == TargetType.Self ? source : destination;
            request.Effect = effect;
            return effectsEntity;
        }
        
        
#if ENABLE_IL2CPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Transform GetViewInstance(this ProtoEntity entity, ProtoWorld world, int viewInstanceId)
        {
            var viewInstancePool = world.GetPool<ViewInstanceTypeComponent>();
            if (viewInstancePool.Has(entity))
            {
                ref var viewInstanceComponent = ref viewInstancePool.Get(entity);
                var viewInstances = viewInstanceComponent.value;
                if (viewInstances != null)
                {
                    var index = viewInstanceId % viewInstances.Length;
                    return viewInstances[index];
                }
            }

            var transformPool = world.GetPool<TransformComponent>();
            if (transformPool.Has(entity))
            {
                ref var transformComponent = ref transformPool.Get(entity);
                return transformComponent.Value;
            }

            return default;
        }
    }
}