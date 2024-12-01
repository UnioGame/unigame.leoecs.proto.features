namespace UniGame.Ecs.Proto.TargetSelection
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Selection;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Target/Target Selection")]
    public class TargetSelectionFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            
            var targetSelectionSystem = new TargetSelectionSystem();
            world.SetGlobal(targetSelectionSystem);
            
            ecsSystems.Add(targetSelectionSystem);
            //collect all valida target into targets component
            ecsSystems.Add(new InitKdTreeTargetsSystem());
            ecsSystems.Add(new CollectKdTreeTargetsSystem());
            ecsSystems.Add(new SelectAreaTargetsSystem());
            return UniTask.CompletedTask;
        }
    }
}