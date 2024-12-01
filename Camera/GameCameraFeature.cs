namespace UniGame.Ecs.Proto.Camera
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Camera Feature", fileName = "Camera Feature")]
    public class GameCameraFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new CameraLookAtTargetSystem());
            return UniTask.CompletedTask;
        }
    }
}