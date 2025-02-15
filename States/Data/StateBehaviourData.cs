namespace Game.Ecs.State.Converters
{
    using System;
    using Data;
    using UnityEngine;

    [Serializable]
    public class StateBehaviourData
    {
        public StateId stateId;
        
        [SerializeReference]
        public IStateBehaviour stateBehaviour;
    }
}