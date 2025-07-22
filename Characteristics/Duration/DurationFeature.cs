namespace UniGame.Ecs.Proto.Characteristics.Duration
{
    using System;
    using Components;
    using Systems;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Duration Feature")]
    public sealed class DurationFeature : CharacteristicFeature<DurationEcsFeature,DurationComponent>
    {
    }
    
    [Serializable]
    public sealed class DurationEcsFeature : CharacteristicEcsFeature<DurationComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateDurationSystem());
            ecsSystems.DelHere<RecalculateDurationRequest>();
            ecsSystems.Add(new ResetDurationSystem());
            return UniTask.CompletedTask;
        }
    }
}