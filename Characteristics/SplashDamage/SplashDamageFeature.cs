namespace UniGame.Ecs.Proto.Characteristics.SplashDamage
{
    using System;
    using Components;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    /// <summary>
    /// allows you to deal damage on the area with default attacks, 
    /// the characteristic increases the damage that opponents receive near the main target
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/SplashDamage Feature")]
    public sealed class SplashDamageFeature : CharacteristicFeature<SplashDamageEcsFeature,SplashDamageComponent>
    {
    }
    
    [Serializable]
    public sealed class SplashDamageEcsFeature : CharacteristicEcsFeature<SplashDamageComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            // update ability power value
            ecsSystems.Add(new RecalculateSplashDamageSystem());
            
            return UniTask.CompletedTask;
        }
    }
}