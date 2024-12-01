namespace UniGame.Ecs.Proto.Movement
{
    using Systems.NavMesh.Animation;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Movement/NavMesh Movement Animation Feature", 
        fileName = "NavMesh Movement Animation Feature")]
    public sealed class NavMeshMovementAnimationFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new NavMeshMovementAnimationSystem());
            return UniTask.CompletedTask;
        }
    }
}