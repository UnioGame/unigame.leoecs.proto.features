namespace UniGame.Ecs.Proto.Characteristics.AttackDamage
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
    /// new characteristic feature: AttackDamage 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/AttackDamage")]
    public sealed class AttackDamageFeature : CharacteristicFeature<AttackDamageEcsFeature,AttackDamageComponent>
    {
    }

    /// <summary>
    /// new characteristic feature: AttackDamage 
    /// </summary>
    [Serializable]
    public sealed class AttackDamageEcsFeature : CharacteristicEcsFeature<AttackDamageComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update AttackDamage by request
            ecsSystems.Add(new ProcessAttackDamageChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}