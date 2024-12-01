namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using AI.Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Components;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class ClearMoveToTargetsSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private MoveToTargetAspect _moveToTargetAspect;
        
        private ProtoIt _filter = It
            .Chain<AiAgentComponent>()
            .Inc<MoveToGoalComponent>()
            .Inc<MoveToTargetPlannerComponent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var goalComponent = ref _moveToTargetAspect.ToGoal.Get(entity);
                goalComponent.Goals.Clear();
            }
        }
    }
}