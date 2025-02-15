namespace Game.Modules.SequenceActions.Data
{
    using System;

    [Serializable]
    public struct SequenceActionResultComponent
    {
        public bool IsDone;
        public bool IsError;
        public string Error;
    }
}