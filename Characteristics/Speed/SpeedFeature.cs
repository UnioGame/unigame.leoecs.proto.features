namespace UniGame.Ecs.Proto.Characteristics.Speed
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using LeoEcs.Shared.Extensions;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// new characteristic feature: Speed 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Speed")]
    public sealed class SpeedFeature : CharacteristicFeature<SpeedEcsFeature,SpeedComponent>
    {
        
    }

    /// <summary>
    /// new characteristic feature: Speed 
    /// </summary>
    [Serializable]
    public sealed class SpeedEcsFeature : CharacteristicEcsFeature<SpeedComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            // update ability power value
            ecsSystems.Add(new ProcessSpeedChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}