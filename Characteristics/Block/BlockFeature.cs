namespace UniGame.Ecs.Proto.Characteristics.Block
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Base;
    using Feature;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Block Feature")]
    public sealed class BlockFeature : CharacteristicFeature<BlockEcsFeature>
    {
    }

    [Serializable]
    public sealed class BlockEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.AddCharacteristic<BlockComponent>();
            ecsSystems.Add(new RecalculateBlockSystem());

            return UniTask.CompletedTask;
        }
    }
}