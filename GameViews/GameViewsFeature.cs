namespace UniGame.Ecs.Proto.Gameplay.LevelProgress
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/GameViews Feature")]
    public class GameViewsFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            //disable active view on activate new one
            ecsSystems.Add(new DisableGameViewOnActivateSystem());
            //disable active view
            ecsSystems.Add(new DisableActiveGameViewSystem());
            ecsSystems.Add(new RemoveActiveViewSystem());
            
            //make request to load new game view by resource id
            ecsSystems.Add(new ActivateGameViewSystem());

            //delete processed requests
            ecsSystems.DelHere<ActivateGameViewRequest>();
            ecsSystems.DelHere<DisableActiveGameViewRequest>();
            
            return UniTask.CompletedTask;
        }
    }
}
