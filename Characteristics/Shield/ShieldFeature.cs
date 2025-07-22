namespace UniGame.Ecs.Proto.Characteristics.Shield
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

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Shield Feature")]
    public sealed class ShieldFeature : CharacteristicFeature<ShieldEcsFeature,ShieldComponent>
    {
    }
    
    [Serializable]
    public sealed class ShieldEcsFeature : CharacteristicEcsFeature<ShieldComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessShieldSystem());
            ecsSystems.DelHere<ChangeShieldRequest>();
            // update ability power value
            ecsSystems.Add(new ResetShieldSystem());
            return UniTask.CompletedTask;
        }
    }
}