namespace UniGame.Ecs.Proto.GameAi.MoveToTarget.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Game.Ecs.Core.Death.Components;
    using Game.Modules.leoecs.proto.features.Ai.Ai.Variants.MoveToTarget.Aspects;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public sealed class ResetPoiSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private MoveToTargetAspect _moveToTargetAspect;
        
        private ProtoIt _filter = It
            .Chain<MoveToPoiGoalsComponent>()
            .Inc<DisabledEvent>()
            .End();
        
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var goals = ref _moveToTargetAspect.ToPoiGoals.Get(entity);
                var keys = goals.GoalsLinks.Keys.ToArray();
                foreach (var key in keys)
                {
                    var data = goals.GoalsLinks[key];
                    data.Complete = false;
                    goals.GoalsLinks[key] = data;
                }
            }
        }
    }
}