namespace UniGame.Ecs.Proto.Characteristics.Feature
{
    using System;
    using Base;
    using Base.Aspects;
    using Cysharp.Threading.Tasks;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public abstract class CharacteristicFeature<TFeature,TComponent> : CharacteristicFeature<TFeature>
        where TFeature : CharacteristicEcsFeature<TComponent>,new()
        where TComponent : struct
    {

    }
    
    public abstract class CharacteristicFeature<TFeature> : BaseLeoEcsFeature
        where TFeature : CharacteristicEcsFeature,new()
    {
        private TFeature _feature = new();
        
        public sealed override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            return _feature.InitializeAsync(ecsSystems);
        }
    }
    
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public abstract class CharacteristicEcsFeature<TComponent> : CharacteristicEcsFeature
        where TComponent : struct
    {
        protected sealed override async UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<TComponent>();

            await OnCharacteristicInitializeAsync(ecsSystems);
        }
        
        protected abstract UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems);
        
        
        [Serializable]
        public class CharacteristicFeatureAspect: GameCharacteristicAspect<TComponent> 
        {
            
        }
    }
    
    [Serializable]
    public abstract class CharacteristicEcsFeature: EcsFeature{}

    
}