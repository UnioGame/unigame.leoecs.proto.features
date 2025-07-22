namespace UniGame.Ecs.Proto.Characteristics.Dodge
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

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Dodge Feature")]
    public sealed class DodgeFeature : CharacteristicFeature<DodgeEcsFeature,DodgeComponent>
    {
    }
    
    [Serializable]
    public sealed class DodgeEcsFeature : CharacteristicEcsFeature<DodgeComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateDodgeSystem());
            return UniTask.CompletedTask;
        }
    }
}