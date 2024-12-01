namespace UniGame.Ecs.Proto.Characteristics.Radius
{
    using System;
    using Base;
    using Systems;
    using Component;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Radius Feature")]
    public sealed class RadiusFeature : CharacteristicFeature<RadiusEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class RadiusEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<RadiusComponent>();
            ecsSystems.Add(new RecalculateRadiusSystem());
            return UniTask.CompletedTask;
        }
    }
}