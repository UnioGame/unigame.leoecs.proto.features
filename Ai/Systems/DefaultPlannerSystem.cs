namespace UniGame.Ecs.Proto.AI.Systems
{
    using System;
    using Aspects;
    using Components;
    using LeoEcs.Bootstrap.Runtime.Attributes;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using Service;

    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DefaultPlannerSystem : BasePlannerSystem<AiDefaultActionComponent>, IProtoInitSystem
    {
#if ODIN_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        [SerializeField]
        private AiPlannerData _plannerData;

        private ProtoWorld _world;
        private IProtoSystems _systems;
        private AiAspect _aiAspect;

        private ProtoIt _filter = It
            .Chain<AiAgentComponent>()
            .End();

        public void Init(IProtoSystems systems)
        {
            _systems = systems;
            _world = systems.GetWorld();
        }

        public override void Run()
        {
            foreach (var entity in _filter)
            {
                if (!IsPlannerEnabledForEntity(_world, entity))
                    continue;

                _aiAspect.AiDefaultAction.GetOrAddComponent(entity);
                ApplyPlanningResult(_systems, entity, _plannerData);
            }
        }
    }
}