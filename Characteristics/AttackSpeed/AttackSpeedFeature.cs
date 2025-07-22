namespace UniGame.Ecs.Proto.Characteristics.AttackSpeed
{
    using System;
    using Base;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using LeoEcs.Shared.Extensions;
    using Leopotam.EcsProto;
    using Systems;
    using UnityEngine;

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/AttackSpeed Feature")]
    public sealed class AttackSpeedFeature : CharacteristicFeature<AttackSpeedEcsFeature,AttackSpeedComponent>
    {
        
    }
    
    [Serializable]
    public sealed class AttackSpeedEcsFeature : CharacteristicEcsFeature<AttackSpeedComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update attack speed value
            ecsSystems.Add(new UpdateAttackSpeedChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}