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
        [Tooltip("How many entities with Health can be targeted at once. Extra ones are not selectable.")]
        [Min(1)]
        public int maxAgents = 512;

        [Tooltip("How many targets one area query returns.")]
        [Min(1)]
        public int maxTargets = 64;

        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            // before any system allocates buffers by these sizes; put this feature before features that select targets (Ai)
            TargetSelectionData.MaxAgents = maxAgents;
            TargetSelectionData.MaxTargets = maxTargets;

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