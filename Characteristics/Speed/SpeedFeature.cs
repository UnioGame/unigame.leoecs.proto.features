namespace UniGame.Ecs.Proto.Characteristics.Speed
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
    /// new characteristic feature: Speed 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Speed")]
    public sealed class SpeedFeature : CharacteristicFeature<SpeedEcsFeature>
    {

    }

    /// <summary>
    /// new characteristic feature: Speed 
    /// </summary>
    [Serializable]
    public sealed class SpeedEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register Speed characteristic
            ecsSystems.AddCharacteristic<SpeedComponent>();
            //update Speed by request
            ecsSystems.Add(new ProcessSpeedChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}