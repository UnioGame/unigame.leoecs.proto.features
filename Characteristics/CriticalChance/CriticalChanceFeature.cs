﻿namespace UniGame.Ecs.Proto.Characteristics.CriticalChance
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
    /// - recalculate attack speed characteristic
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/Critical Chance Feature")]
    public sealed class CriticalChanceFeature : CharacteristicFeature<CriticalChanceEcsFeature,CriticalChanceComponent>
    {
    }

    [Serializable]
    public sealed class CriticalChanceEcsFeature : CharacteristicEcsFeature<CriticalChanceComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update attack speed value
            ecsSystems.Add(new UpdateCriticalChanceChangedSystem());

            return UniTask.CompletedTask;
        }
    }
}