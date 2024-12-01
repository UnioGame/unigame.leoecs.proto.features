namespace UniGame.Ecs.Proto.Ability.UserInput
{
    using System;
    using Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "ECS Proto/Features/Ability/Ability Input Feature", 
        fileName = "Ability Input Feature")]
    public sealed class AbilityUserInputFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new ProcessAbilityUpInputSystem());
            ecsSystems.Add(new RestoreDefaultInHandAbilitySystem());
            ecsSystems.Add(new ClearActiveTimeSystem());

            return UniTask.CompletedTask;
        }
    }
}