namespace UniGame.Ecs.Proto.AI
{
    using Components;
    using Configurations;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS Proto/Features/Ai Feature", fileName = "Ai Feature")]
    public sealed class AIFeature : BaseLeoEcsFeature
    {
        [SerializeField]
        public AiConfigurationAsset aiConfigurationAsset;
        
        public override async UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            var configurationAsset = Instantiate(aiConfigurationAsset);
            var configuration = configurationAsset.configuration;
            var actions = configuration.aiActions;

            //add all planners
            for (var index = 0; index < actions.Length; index++)
            {
                var aiActionData = actions[index];
                var planner = aiActionData.planner;
                await planner.Initialize(index,ecsSystems);
            }

            //collect ai agents info
            //ecsSystems.Add(new AiCollectPlannerDataSystem(configuration));
            //make ai planning by ai agent data
            ecsSystems.Add(new AiPlanningSystem());
            //apply specific actions components
            ecsSystems.Add(new AiUpdatePlanningActionsStatusSystem(configuration.aiActions));

            //add all actions
            foreach (var aiActionData in configuration.aiActions)
                ecsSystems.Add(aiActionData.action);

            ecsSystems.Add(new AiCleanUpPlanningDataSystem(actions));
            
            //remove remove plan data
            ecsSystems.DelHere<AiAgentPlanningComponent>();
        }
    }
}