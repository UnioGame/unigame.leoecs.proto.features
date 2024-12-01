namespace UniGame.Ecs.Proto.Characteristics.Mana
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

    /// <summary>
    /// new characteristic feature: Mana 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Mana")]
    public sealed class ManaFeature : CharacteristicFeature<ManaEcsFeature>
    {

    }

    /// <summary>
    /// new characteristic feature: Mana 
    /// </summary>
    [Serializable]
    public sealed class ManaEcsFeature : CharacteristicEcsFeature
    {
        protected override UniTask OnInitializeAsync(IProtoSystems ecsSystems)
        {
            //register Mana characteristic
            ecsSystems.AddCharacteristic<ManaComponent>();
            //update Mana by request
            ecsSystems.Add(new ProcessManaChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}