namespace Game.Modules.SequenceActions
{
    using System;
    using Abstract;
    using UnityEngine;

    [Serializable]
    public struct SequenceActionData
    {
        public float progressWeight;

        [SerializeReference]
        public ISequenceAction action;
        
        public string ActionName => action == null ? "None" : action.ActionName;
    }
}