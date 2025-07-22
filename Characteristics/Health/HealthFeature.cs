namespace UniGame.Ecs.Proto.Characteristics.Health
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using LeoEcs.Shared.Extensions;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// new characteristic feature: Health 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Health")]
    public sealed class HealthFeature : CharacteristicFeature<HealthEcsFeature,HealthComponent>
    {
    }

    /// <summary>
    /// new characteristic feature: Health 
    /// </summary>
    [Serializable]
    public sealed class HealthEcsFeature : CharacteristicEcsFeature<HealthComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update Health by request
            ecsSystems.Add(new ProcessHealthChangedSystem());
            return UniTask.CompletedTask;
        }
    }
}