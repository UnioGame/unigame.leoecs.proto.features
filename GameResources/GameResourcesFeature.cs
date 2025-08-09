namespace UniGame.Ecs.Proto.GameResources
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using Context.Runtime;
    using GameDb.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Resources/Game Resources Feature", 
        fileName = "Game Resources Feature")]
    public class GameResourcesFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var context = ecsSystems.GetShared<IContext>();
            var dataBase = await context.ReceiveFirstAsync<IGameDatabase>();
            
            ecsSystems.SetGlobal(dataBase);

            //get new request to spawn game resource
            ecsSystems.Add(new ProcessSpawnRequestSystem(dataBase));
            ecsSystems.DelHere<GameResourceSpawnRequest>();

            ecsSystems.Add(new LoadTaskObserverSystem());

            ecsSystems.Add(new CreateSpawnObjectSystem());
            ecsSystems.DelHere<ResourceInstanceSpawnRequest>();

            ecsSystems.Add(new PoolObjectSystem());
        }
    }
}