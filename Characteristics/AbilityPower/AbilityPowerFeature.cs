namespace UniGame.Ecs.Proto.Characteristics.AbilityPower
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    /// <summary>
    /// provides a feature to increase the damage of abilities,
    /// allows you to change the strength of abilities by AbilityPowerComponent
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/AbilityPower Feature")]
    public sealed class AbilityPowerFeature : CharacteristicFeature<AbilityPowerEcsFeature,AbilityPowerComponent>
    {
    }
    
    [Serializable]
    public sealed class AbilityPowerEcsFeature : CharacteristicEcsFeature<AbilityPowerComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            // update ability power value
            ecsSystems.Add(new RecalculateAbilityPowerSystem());
            
            return UniTask.CompletedTask;
        }
        
    }
}