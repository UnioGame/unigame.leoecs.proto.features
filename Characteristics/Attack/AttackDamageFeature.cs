namespace UniGame.Ecs.Proto.Characteristics.AttackDamage
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
    /// new characteristic feature: AttackDamage 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/AttackDamage")]
    public sealed class AttackDamageFeature : CharacteristicFeature<AttackDamageEcsFeature>
    {

    }

    /// <summary>
    /// new characteristic feature: AttackDamage 
    /// </summary>
    [Serializable]
    public sealed class AttackDamageEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register AttackDamage characteristic
            ecsSystems.AddCharacteristic<AttackDamageComponent>();
            //update AttackDamage by request
            ecsSystems.Add(new ProcessAttackDamageChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}