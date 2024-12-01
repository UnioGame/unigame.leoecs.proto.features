namespace UniGame.Ecs.Proto.AbilityAgent.Converters
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Game.Code.Configuration.Runtime.Ability;
    using Game.Modules.leoecs.proto.tools.Ownership.Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class AbilityAgentConverter : LeoEcsConverter
    {
        #region Inspector
        
        public List<AbilityCell> abilityForAgents = new List<AbilityCell>();
        public EcsEntityConverter entityConfiguration;

        #endregion
        
        public override void Apply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            var packedEntity = entity.PackEntity(world);
            ref var abilityAgentConfiguration = ref world.GetOrAddComponent<AbilityAgentConfigurationComponent>(entity);
            abilityAgentConfiguration.Value = entityConfiguration;
            ref var ownerTransformComponent = ref world.GetComponent<TransformComponent>(entity);
            ref var ownerGameObjectComponent = ref world.GetComponent<GameObjectComponent>(entity);
            
            foreach (var abilityId in abilityForAgents)
            {
                var entityAgent = world.NewEntity();
                ref var abilityAgentComponent = ref world.AddComponent<AbilityAgentComponent>(entityAgent);
                abilityAgentComponent.Value = abilityId;
                ref var ownerLinkComponent = ref world.AddComponent<OwnerLinkComponent>(entityAgent);
                ownerLinkComponent.Value = packedEntity;
                
                //workaround for simultaneous work of ability agent and ability initiator
                ref var agentUnitOwnerComponent = ref world.AddComponent<AbilityAgentUnitOwnerComponent>(entityAgent);
                agentUnitOwnerComponent.Value = entity.PackEntity(world);
                
                ref var transformComponent = ref world.AddComponent<TransformComponent>(entityAgent);
                transformComponent.Value = ownerTransformComponent.Value;
                ref var gameObjectComponent = ref world.AddComponent<GameObjectComponent>(entityAgent);
                gameObjectComponent.Value = ownerGameObjectComponent.Value;
                
                foreach (var entityConverterConverter in entityConfiguration.converters)
                {
                    entityConverterConverter.converter.Apply(world, entityAgent);
                }
            }
        }
    }
}