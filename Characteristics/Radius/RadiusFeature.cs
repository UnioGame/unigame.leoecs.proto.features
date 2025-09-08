namespace UniGame.Ecs.Proto.Characteristics.Radius
{
    using System;
    using Systems;
    using Component;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Radius Feature")]
    public sealed class RadiusFeature : CharacteristicFeature<RadiusEcsFeature,RadiusComponent>
    {
    }
    
    [Serializable]
    public sealed class RadiusEcsFeature : CharacteristicEcsFeature<RadiusComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateRadiusSystem());
            return UniTask.CompletedTask;
        }
    }
}