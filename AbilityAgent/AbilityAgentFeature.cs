namespace UniGame.Ecs.Proto.AbilityAgent
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Ability/Ability Agent Feature", fileName = "Ability Agent Feature")]
    public class AbilityAgentFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new InitializeAbilityAgentSystem());
            ecsSystems.Add(new CreateAbilityAgentSystem());
        }
    }
}