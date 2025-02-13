namespace Game.Ecs.State.Converters
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Components;
    using Leopotam.EcsProto;
    using UniGame.LeoEcs.Shared.Extensions;
    using Data;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniModules.UniGame.Core.Runtime.DataStructure;
    using UnityEngine;

    /// <summary>
    /// Converter that can be used to apply a state to a GameObject.
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class StateBehavioursConverter : GameObjectConverter
    {
        public List<StateBehaviourData> behaviours = new();

        protected override void OnApply(GameObject target, ProtoWorld world, ProtoEntity entity)
        {
            var behaviourEntity = GameStateBehaviourAspect.CreateStateBehaviourEntity(entity, world);
            ref var behavioursComponent = ref world.GetComponent<StateBehavioursMapComponent>(behaviourEntity);
            
            foreach (var behaviour in behaviours)
            {
                behavioursComponent.Behaviours.Add(behaviour.stateId, behaviour.stateBehaviour);
            }
        }

    }
    
    [Serializable]
    public class StateBehaviourData
    {
        public StateId stateId;
        
        [SerializeReference]
        public IStateBehaviour stateBehaviour;
    }
}