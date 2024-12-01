namespace UniGame.Ecs.Proto.Characteristics.CriticalChance
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

    /// <summary>
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/AttackRange Feature")]
    public sealed class AttackRangeFeature : CharacteristicFeature<AttackRangeEcsFeature>
    {
    }
    
    [Serializable]
    public sealed class AttackRangeEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register health characteristic
            ecsSystems.AddCharacteristic<AttackRangeComponent>();
            //update attack speed value
            ecsSystems.Add(new UpdateAttackRangeChangedSystem());
            
            return UniTask.CompletedTask;
        }
    }
}