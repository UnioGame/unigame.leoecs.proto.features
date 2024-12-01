namespace UniGame.Ecs.Proto.Gameplay.Death
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Gameplay/Death")]
    public class DeathFeature  : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            //if unit ready to death then create KillRequest
            ecsSystems.Add(new DetectReadyToDeathByHealthSystem());
            ecsSystems.Add(new ProcessDeathAnimationSystem());
            ecsSystems.Add(new EvaluateDeathAnimationSystem());
            ecsSystems.Add(new CompleteDeathAnimationSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
