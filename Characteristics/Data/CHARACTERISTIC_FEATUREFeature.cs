namespace UniGame.Proto.Features.Characteristics
{
    using System;
    using Cysharp.Threading.Tasks;
    using Ecs.Proto.Characteristics.Base.Aspects;
    using Leopotam.EcsProto;
    using UniGame.Ecs.Proto.Characteristics.Feature;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/CHARACTERISTIC_FEATURE Feature")]
    public sealed class CHARACTERISTIC_FEATUREFeature 
        : CharacteristicFeature<CHARACTERISTIC_FEATUREEcsFeature,CHARACTERISTIC_FEATUREComponent> { }
    
    [Serializable]
    public sealed class CHARACTERISTIC_FEATUREEcsFeature : CharacteristicEcsFeature<CHARACTERISTIC_FEATUREComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
    }
    
    [Serializable]
    public sealed class CHARACTERISTIC_FEATURECharacteristicAspect : GameCharacteristicAspect<CHARACTERISTIC_FEATUREComponent>
    {
        public ProtoPool<CHARACTERISTIC_FEATUREComponent> AttackSpeedComponent;
    }

    public struct CHARACTERISTIC_FEATUREComponent
    {
        
    }
}