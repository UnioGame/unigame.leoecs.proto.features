namespace Game.Modules.SequenceActions.Data
{
    using System;

    [Serializable]
    public struct SequenceActionResult
    {
        public static readonly SequenceActionResult Success = new SequenceActionResult
        {
            IsSuccess = true,
            IsFinished = true,
            Error = string.Empty,
            Message = string.Empty,
            Progress = 1f
        };

        public static readonly SequenceActionResult None = new SequenceActionResult
        {
            IsSuccess = false,
            IsFinished = false,
            Error = string.Empty,
            Message = string.Empty,
            Progress = 0f
        };
        
        public bool IsSuccess;
        public bool IsFinished;
        public string Error;
        public string Message;
        public float Progress;
    }
}