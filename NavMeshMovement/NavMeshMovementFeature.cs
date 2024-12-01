namespace UniGame.Ecs.Proto.Movement
{
    using Systems;
    using Systems.Converters;
    using Systems.NavMesh;
    using Systems.Transform;
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Core.Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "ECS Proto/Features/Movement/NavMesh Movement Feature", fileName = "NavMesh Movement Feature")]
    public sealed class NavMeshMovementFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeAsync(IProtoSystems ecsSystems)
        {
            ecsSystems.Add(new StopNavMeshAgentSystem());
            ecsSystems.Add(new ProcessImmobilityStatusSystem());
            
            ecsSystems.DelHere<VelocityComponent>();
            ecsSystems.Add(new DirectionVelocityConvertSystem());
            
            ecsSystems.Add(new VelocityNavMeshTargetConvertSystem());

            ecsSystems.Add(new NavMeshTargetMovementSystem());
            ecsSystems.DelHere<MovementPointSelfRequest>();
            
            ecsSystems.Add(new NavMeshMovementSystem());
            
            ecsSystems.Add(new RotationToPointSystem());
            ecsSystems.DelHere<RotateToPointSelfRequest>();

            ecsSystems.Add(new ComeToTheEndOfSystem());
            ecsSystems.Add(new RevokeComeToTheEndOfSystem());
            ecsSystems.DelHere<RevokeComeToEndOfRequest>();
            
            ecsSystems.Add(new NavMeshAgentStopSimulationSystem());
            ecsSystems.DelHere<MovementStopSelfRequest>();

            ecsSystems.Add(new DisableNavMeshAgentSystem());
            ecsSystems.Add(new SetNavigationStatusByRequestSystem());
            ecsSystems.Add(new MakeKinematicByRequestSystem());
            
            return UniTask.CompletedTask;
        }
    }
}