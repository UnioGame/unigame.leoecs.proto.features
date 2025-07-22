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
    public sealed class CriticalMultiplierFeature 
        : CharacteristicFeature<CriticalMultiplierEcsFeature,CriticalMultiplierComponent>
    {
    }
    
    [Serializable]
    public sealed class CriticalMultiplierEcsFeature : CharacteristicEcsFeature<CriticalMultiplierComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update attack speed value
            ecsSystems.Add(new UpdateCriticalChanceChangedSystem());
            return UniTask.CompletedTask;
        }
    }
}