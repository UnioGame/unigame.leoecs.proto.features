namespace Game.Ecs.State.Converters
{
    using System;
    using Data;
    using UnityEngine;

    [Serializable]
    public class StateBehaviourData
    {
        public string Name => stateBehaviour == null ? "None" : stateBehaviour.GetType().Name;

        public StateId stateId;
        
        [SerializeReference]
        public IStateBehaviour stateBehaviour;
    }
}