namespace UniGame.Ecs.Proto.AbilityAgent.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using Leopotam.EcsProto.QoL;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

    /// <summary>
    /// Create ability agent system
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateAbilityAgentSystem : IProtoRunSystem
    {
        private ProtoWorld _world;
        private AbilityAgentAspect _abilityAgentAspect;
        private UnityAspect _unityAspect;

        private ProtoIt _filter = It
            .Chain<CreateAbilityAgentSelfRequest>()
            .End();

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _abilityAgentAspect.CreateAbilityAgentSelfRequest.Get(entity);
                if (!request.Owner.Unpack(_world, out var ownerEntity))
                    continue;
                ref var abilityAgentComponent = ref _abilityAgentAspect.AbilityAgentComponent.Add(entity);
                abilityAgentComponent.Value = request.AbilityCell;
                ref var agentUnitOwnerComponent = ref _abilityAgentAspect.AbilityAgentUnitOwnerComponent.Add(entity);
                agentUnitOwnerComponent.Value = request.Owner;
                
                ref var ownerTransformComponent = ref _unityAspect.Transform.Get(ownerEntity);
                ref var transformComponent = ref _unityAspect.Transform.Add(entity);
                transformComponent.Value = ownerTransformComponent.Value;
                
                ref var ownerGameObjectComponent = ref _unityAspect.GameObject.Get(ownerEntity);
                ref var gameObjectComponent = ref _unityAspect.GameObject.Add(entity);
                gameObjectComponent.Value = ownerGameObjectComponent.Value;
                
                ref var abilityAgentConfiguration = ref _abilityAgentAspect.AbilityAgentConfigurationComponent.Get(ownerEntity);
                var entityConfiguration = abilityAgentConfiguration.Value;
                
                foreach (var entityConverterConverter in entityConfiguration.converters)
                {
                    entityConverterConverter.converter.Apply(_world, entity);
                }
                
                _abilityAgentAspect.CreateAbilityAgentSelfRequest.Del(entity);
            }
        }
    }
}