namespace UniGame.Ecs.Proto.Characteristics.ArmorResist
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

    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Armor Resist Feature")]
    public class ArmorResistFeature : CharacteristicFeature<ArmorResistEcsFeature,ArmorResistComponent>
    {
    }

    [Serializable]
    public sealed class ArmorResistEcsFeature : CharacteristicEcsFeature<ArmorResistComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            // update armor resist value
            ecsSystems.Add(new UpdateArmorResistSystem());
            return UniTask.CompletedTask;
        }
    }
}