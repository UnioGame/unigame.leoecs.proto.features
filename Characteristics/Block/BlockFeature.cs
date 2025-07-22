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
    public sealed class BlockFeature : CharacteristicFeature<BlockEcsFeature,BlockComponent>
    {
        
    }

    [Serializable]
    public sealed class BlockEcsFeature : CharacteristicEcsFeature<BlockComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateBlockSystem());

            return UniTask.CompletedTask;
        }
    }
}