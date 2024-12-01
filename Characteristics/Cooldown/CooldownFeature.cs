namespace UniGame.Ecs.Proto.Characteristics.Cooldown
{
    using System;
    using Systems;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Cooldown Feature")]
    public sealed class CooldownFeature : CharacteristicFeature<CooldownEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class CooldownEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new RecalculateCooldownSystem());
            ecsSystems.DelHere<RecalculateCooldownSelfRequest>();
            ecsSystems.Add(new ResetCooldownSystem());
            
            return UniTask.CompletedTask;
        }
    }
}