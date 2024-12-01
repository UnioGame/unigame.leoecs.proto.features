namespace UniGame.Ecs.Proto.Characteristics.CriticalMultiplier
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Base;
    using Feature;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Critical Multiplier Feature")]
    public sealed class CriticalMultiplierFeature : CharacteristicFeature<CriticalMultiplierEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class CriticalMultiplierEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<CriticalMultiplierComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateCriticalChanceChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}