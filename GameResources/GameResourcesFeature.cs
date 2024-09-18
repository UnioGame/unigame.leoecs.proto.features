namespace UniGame.Ecs.Proto.GameResources
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Code.DataBase.Runtime.Abstract;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.Context.Runtime.Extension;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Proto Features/Resources/Game Resources Feature", fileName = "Game Resources Feature")]
    public class GameResourcesFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var context = ecsSystems.GetShared<IContext>();
            var dataBase = await context.ReceiveFirstAsync<IGameDatabase>();
            var world = ecsSystems.GetWorld();

            var spawnTools = new GameSpawnTools();
            world.SetGlobal(spawnTools);
            ecsSystems.Add(spawnTools);
            
            //remove event when it complete
            ecsSystems.DelHere<GameResourceSpawnCompleteEvent>();
            
            //kill GameResourceTaskCompleteEvent event
            ecsSystems.Add(new DestroyCompletedResourceTaskSystem());
            
            //get spawn request and create resource loading task
            ecsSystems.Add(new ProcessSpawnRequestSystem());
            
            //load addressable resource by id add resource task component
            //when resource loaded add GameResourceResultComponent to entity
            ecsSystems.Add(new ProceedGameResourceRequestSystem(dataBase,context.LifeTime));
            
            //if gameobject contains navmeshagent fix spawn position
            ecsSystems.Add(new FixNavMeshResourcePositionSpawnSystem());
            
            //create spawn object by GameResourceResultComponent
            ecsSystems.Add(new CreateSpawnObjectSystem());
            //handle non gameobject assets
            ecsSystems.Add(new ApplySpawnGameObjectSystem());
            
            //mark resource as loaded
            ecsSystems.Add(new CompleteGameResourceObjectSystem());
            
            //fire GameResourceTaskCompleteEvent when GameResourceResultComponent added
            ecsSystems.Add(new MarkResourceTaskAsCompleteSystem());

            //delete all processed spawn requests
            ecsSystems.DelHere<GameResourceSpawnRequest>();
        }
    }
}