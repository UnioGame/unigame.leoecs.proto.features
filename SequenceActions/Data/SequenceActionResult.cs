namespace Game.Modules.SequenceActions.Data
{
    using System;

    [Serializable]
    public struct SequenceActionResult
    {
        public bool IsDone;
        public bool IsError;
        public string Error;
        public float Progress;
    }
}