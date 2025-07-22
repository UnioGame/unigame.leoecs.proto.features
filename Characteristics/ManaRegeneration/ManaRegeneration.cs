namespace UniGame.Ecs.Proto.Characteristics.ManaRegeneration
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
    /// new characteristic feature: ManaRegeneration 
    /// </summary>
    [CreateAssetMenu(menuName = "ECS Proto/Features/Characteristics/ManaRegeneration")]
    public sealed class ManaRegenerationFeature : CharacteristicFeature<ManaRegenerationEcsFeature,ManaRegenerationComponent>
    {
    }

    /// <summary>
    /// new characteristic feature: ManaRegeneration 
    /// </summary>
    [Serializable]
    public sealed class ManaRegenerationEcsFeature : CharacteristicEcsFeature<ManaRegenerationComponent>
    {
        protected override UniTask OnCharacteristicInitializeAsync(IProtoSystems ecsSystems)
        {
            //update ManaRegeneration by request
            ecsSystems.Add(new ProcessManaRegenerationChangedSystem());
            
            // Mana regeneration. Uses request ChangeManaRequest when you want to change mana value.
            // Inside uses a timer. 
            ecsSystems.Add(new ProcessManaRegenerationSystem());

            return UniTask.CompletedTask;
        }
    }
}