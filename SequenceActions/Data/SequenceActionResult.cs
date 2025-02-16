namespace Game.Modules.SequenceActions.Data
{
    using System;
    using UnityEngine.Serialization;

    [Serializable]
    public struct SequenceActionResult
    {
        public bool IsSuccess;
        public bool IsFinished;
        public string Error;
        public string Message;
        public float Progress;
    }
}